using System;

namespace ML.Utils.DataFeed.DiscountWatchStore
{
    [Serializable]
    public class ProductPrice
    {
        public string Currency { get; set; } = "USD";

        public double Amount { get; set; }

        public double? RetailAmount { get; set; }
    }
}
