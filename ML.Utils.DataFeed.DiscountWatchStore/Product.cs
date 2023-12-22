using System;
using System.Collections.Generic;

namespace ML.Utils.DataFeed.DiscountWatchStore
{
    [Serializable]
    public class Product
    {
        public Product(ProductType type = ProductType.Watch)
        {
            Type = type;

            Images = new List<ProductImage>();

            Price = new ProductPrice();

            //Specification = new ProductSpecification();

            Attributes = new List<ProductAttribute>();
        }

        public Product(DataFeed feed) : this(feed.ProductType)
        {
            Url = feed.Url;
            Brand = feed.Brand;
            Category = feed.Category;
        }

        public ProductType Type { get; set; }

        public string Url { get; set; }

        public Brand Brand { get; set; }

        public string Category { get; set; }

        public string Key { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public List<ProductImage> Images { get; set; }

        public ProductPrice Price { get; set; }

        public string Description { get; set; }

        //public ProductSpecification Specification { get; set; }

        public List<ProductAttribute> Attributes { get; set; }
    }
}
