
using AutoMapper;
using ML.Common;
using ML.Utils.DataFeed.DiscountWatchStore;
using ML.Utils.MongoDb;
using ML.Utils.ShareFile;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Entity = DiscountWatchStore.DataContext;
using EntityInc = DiscountWatchStore.DataContext.Inc;

namespace DiscountWatchStore.Console
{
    internal class ProductManager
    {
        private const string FolderProducts = "Products";

        private readonly ShareFileManager _shareFile;

        private readonly Entity.DWSDataContext _db;

        public ProductManager()
        {
            var appSettings = ConfigurationManager.AppSettings;

            _shareFile = new ShareFileManager(new ShareSetting
            {
                TempPath = appSettings["FILESHARE_TEMP_PATH"],
                ShareRootPath = appSettings["FILESHARE_ROOT_FILEPATH"],
                ShareUserName = appSettings["FILESHARE_USER"],
                SharePassword = appSettings["FILESHARE_PASSWORD"],
                ShareDomain = appSettings["FILESHARE_DOMAIN"]
            });

            _db = new Entity.DWSDataContext();

            ConfigAutoMapper();
        }

        public async Task Test()
        {
        }

        public async Task<ObjectId?> SaveAsync(Product product)
        {
            var entity = Mapper.Map<Product, Entity.Product>(product);

            var existEntity = await _db.Products.Find(p => p.Key == entity.Key).Project(p => new { p.Id }).FirstOrDefaultAsync();
            entity.Id = existEntity?.Id ?? entity.Id;

            var result = await _db.Products.SaveAsync(entity);

            if (!result.Ok())
            {
                return null;
            }

            SaveFileImages(product.Images);

            return entity.Id;
        }

        public async Task<ObjectId?> UpdatePriceAsync(Product product)
        {
            var entity = await _db.Products.Find(p => p.Key == product.Key).Project(p => new { p.Id, p.Price }).FirstOrDefaultAsync();

            if (entity == null)
            {
                return null;
            }

            if (!entity.Price.Amount.Equals(product.Price.Amount) || entity.Price.RetailAmount != product.Price.RetailAmount)
            {
                var update = Builders<Entity.Product>.Update
                    .Set(p => p.Price, product.Price.Map<ProductPrice, EntityInc.ProductPrice>())
                    .AddToSet(p => p.PriceLogs, entity.Price.Map<EntityInc.ProductPrice, EntityInc.ProductPriceLog>());

                await _db.Products.UpdateOneAsync(entity.Id, update);
            }

            return entity.Id;
        }

        private void SaveFileImages(List<ProductImage> images)
        {
            foreach (var image in images)
            {
                using (var fileHandle = _shareFile.CreateFile())
                {
                    File.WriteAllBytes(fileHandle.LocalFilePath, image.Contents);

                    _shareFile.SaveFile(FolderProducts, image.FileName, fileHandle);
                }
            }
        }

        private void ConfigAutoMapper()
        {
            Mapper.CreateMap<ProductImage, EntityInc.ProductImage>();

            Mapper.CreateMap<ProductPrice, EntityInc.ProductPrice>();

            Mapper.CreateMap<EntityInc.ProductPrice, EntityInc.ProductPriceLog>();

            Mapper.CreateMap<ProductAttribute, EntityInc.ProductAttribute>();

            Mapper.CreateMap<Product, Entity.Product>()
                .ForMember(d => d.Id, m => m.Ignore())
                .ForMember(d => d.Type, m => m.MapFrom(s => s.Type.ToString()))
                .ForMember(d => d.Brand, m => m.MapFrom(s => s.Brand != null ? s.Brand.Name : string.Empty));
        }
    }
}
