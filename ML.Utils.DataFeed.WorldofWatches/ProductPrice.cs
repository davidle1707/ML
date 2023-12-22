using System;

namespace ML.Utils.DataFeed.WorldofWatches
{
    [Serializable]
    public class ProductPrice
    {
        public string Currency { get; set; } = "USD";

        public double Amount { get; set; }

        public double? ListAmount { get; set; }

        public double? RegAmount { get; set; }
    }
}
