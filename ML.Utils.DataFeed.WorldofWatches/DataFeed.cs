using System;
using System.Collections.Generic;

namespace ML.Utils.DataFeed.WorldofWatches
{
    [Serializable]
    public class DataFeed
    {
        public DataFeed(ProductType productType, string url = "", string category = "Unknown")
        {
            Product = new Product(productType) { Url = url, Category = category };
        }

        public List<Brand> Brands { get; set; } = new List<Brand>();

        public ProductType ProductType => Product.Type;

        /// <summary>
        /// List or Detail
        /// </summary>
        public string Url => Product.Url;

        public string Category => Product.Category;

        internal Product Product { get; set; }

        internal DataFeed Clone(string newUrl)
        {
            return new DataFeed(ProductType, newUrl, Category);
        }
    }
}
