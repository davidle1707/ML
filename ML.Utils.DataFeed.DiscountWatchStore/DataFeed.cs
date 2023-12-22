using System;

namespace ML.Utils.DataFeed.DiscountWatchStore
{
    [Serializable]
    public class DataFeed
    {
        public DataFeed(ProductType productType, string url = "", Brand brand = null, string category = "Unknown")
        {
            Product = new Product(productType) { Url = url, Brand = brand, Category = category };
        }

        public ProductType ProductType => Product.Type;

        public string Url => Product.Url;

        public Brand Brand => Product.Brand;

        public string Category => Product.Category;

        internal Product Product { get; set; }

        internal DataFeed Clone(string newUrl)
        {
            return new DataFeed(ProductType, newUrl, Brand, Category);
        }
    }
}
