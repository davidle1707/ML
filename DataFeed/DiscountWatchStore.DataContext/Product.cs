using DiscountWatchStore.DataContext.Inc;
using System;
using System.Collections.Generic;
using ML.Utils.MongoDb;

namespace DiscountWatchStore.DataContext
{
    [Serializable]
    [CollectionName(typeof(Product))]
    public class Product : BaseEntity
    {
        public string Type { get; set; }

        public string Url { get; set; }

        public string Brand { get; set; }

        public string Category { get; set; }

        public string Key { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public List<ProductImage> Images { get; set; } = new List<ProductImage>();

        public ProductPrice Price { get; set; } = new ProductPrice();

        public string Description { get; set; }

        public List<ProductAttribute> Attributes { get; set; } = new List<ProductAttribute>();

        public List<ProductPriceLog> PriceLogs { get; set; } = new List<ProductPriceLog>();
    }
}
