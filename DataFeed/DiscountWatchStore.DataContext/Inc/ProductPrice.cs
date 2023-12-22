using System;

namespace DiscountWatchStore.DataContext.Inc
{
    [Serializable]
    public class ProductPrice
    {
        public string Currency { get; set; }

        public double Amount { get; set; }

        public double? RetailAmount { get; set; }
    }
}
