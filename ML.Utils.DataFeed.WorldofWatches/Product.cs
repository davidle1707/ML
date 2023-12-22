using System;
using System.Collections.Generic;

namespace ML.Utils.DataFeed.WorldofWatches
{
    [Serializable]
    public class Product
    {
        public Product(ProductType type = ProductType.Watch)
        {
            Type = type;

        }

        public ProductType Type { get; set; }

        public string Url { get; set; }

        public Brand Brand { get; set; }

        public string Category { get; set; }

        public List<string> CategoryBreadcrumbs { get; set; } = new List<string>();

        public string Key { get; set; }

        public string Sku { get; set; }

        public string Name { get; set; }

        public List<ProductImage> Images { get; set; } = new List<ProductImage>();

        public ProductPrice Price { get; set; } = new ProductPrice();

        public string Description { get; set; }

        public List<ProductAttribute> Attributes { get; set; } = new List<ProductAttribute>();
    }
}
