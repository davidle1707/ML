using ML.Utils.DataFeed.DiscountWatchStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscountWatchStore.Console
{
    public class InternalManager
    {
        #region Singleton

        private static readonly Lazy<InternalManager> LazyInstance = new Lazy<InternalManager>(() => new InternalManager());

        public static InternalManager Instance => LazyInstance.Value;

        #endregion

        private readonly ProductManager _product;
        private readonly DiscountWatchStoreManager _manager;

        public InternalManager()
        {
            _product = new ProductManager();
            _manager = new DiscountWatchStoreManager();
        }

        public List<DataFeed> GetDataFeeds(List<ProductType> types, params string[] brandNames)
        {
            var query = DiscountWatchStoreManager.DataFeeds.Where(a => types.Contains(a.ProductType));

            if (brandNames.Length > 0 && brandNames.Length != DiscountWatchStoreManager.Brands.Count)
            {
                query = query.Where(a => a.Brand != null && brandNames.Contains(a.Brand.Name));
            }

            return query.ToList();
        }

        public async Task GetProducts(List<DataFeed> feeds, int? startPage = null, int? startRecordIndex = null, int? delayMillisecondsEachPage = null, int? delayMillisecondsEachProduct = null, RichTextBox rtbReport = null)
        {
            var index = 1;

            var option = new GetOption
            {
                ProductGetFullDetails = true,
                ProductCallBackAsync = async product =>
                {
                    if (rtbReport != null)
                    {
                        rtbReport.AppendText(Environment.NewLine);
                        rtbReport.AppendText($"{index++}. Key: {product.Key} - Name: {product.Name}");
                    }

                    await _product.SaveAsync(product);
                }
            };

            option.StartPage = startPage ?? option.StartPage;
            option.StartRecordIndex = startRecordIndex ?? option.StartRecordIndex;
            option.DelayMillisecondsEachPage = delayMillisecondsEachPage ?? option.DelayMillisecondsEachPage;
            option.DelayMillisecondsEachProduct = delayMillisecondsEachProduct ?? option.DelayMillisecondsEachProduct;


            await _manager.GetProductsAsync(feeds, option);
        }

        public Task UpdatePriceProductsInConsole(List<ProductType> types, params string[] brandKeys)
        {
            var feeds = GetDataFeeds(types, brandKeys);

            return UpdatePriceProducts(feeds);
        }

        public Task UpdatePriceProducts(List<DataFeed> feeds, int? startPage = null, int? startRecordIndex = null, int? delayMillisecondsEachPage = null, int? delayMillisecondsEachProduct = null, RichTextBox rtbReport = null)
        {
            var index = 1;

            var option = new GetOption
            {
                ProductGetFullDetails = false,
                ProductCallBackAsync = async product =>
                {
                    if (rtbReport != null)
                    {
                        rtbReport.AppendText(Environment.NewLine);
                        rtbReport.AppendText($"{index++}. Key: {product.Key} - Name: {product.Name}");
                    }

                    var productId = await _product.UpdatePriceAsync(product);

                    //not exists -> call get product from data feed
                    if (productId == null)
                    {
                        await GetProductFromFeed(new DataFeed(product.Type, product.Url, product.Brand, product.Category));
                    }
                    else
                    {
                        rtbReport?.AppendText($" - ProductId: {productId}");
                    }
                }
            };

            option.StartPage = startPage ?? option.StartPage;
            option.StartRecordIndex = startRecordIndex ?? option.StartRecordIndex;
            option.DelayMillisecondsEachPage = delayMillisecondsEachPage ?? option.DelayMillisecondsEachPage;
            option.DelayMillisecondsEachProduct = delayMillisecondsEachProduct ?? option.DelayMillisecondsEachProduct;


            return _manager.GetProductsAsync(feeds, option);
        }

        private async Task GetProductFromFeed(DataFeed feed, RichTextBox rtbReport = null)
        {
            await _manager.GetProductAsync(feed, new GetOption
            {
                ProductCallBackAsync = async product =>
                {
                    if (rtbReport != null)
                    {
                        rtbReport.AppendText(Environment.NewLine);
                        rtbReport.AppendText($"\t- Get feed {feed.Url}");
                    }

                    var productId = await _product.SaveAsync(product);

                    if (rtbReport != null)
                    {
                        rtbReport.AppendText(Environment.NewLine);
                        rtbReport.AppendText($"\t- ProductId: {productId}");
                    }
                },
            });
        }
    }
}
