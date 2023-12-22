using System;

namespace WorldofWatches.DataContext.Inc
{
    [Serializable]
    public class ProductPrice
    {
        public string Currency { get; set; }

        public double Amount { get; set; }

        public double? ListAmount { get; set; }

        public double? RegAmount { get; set; }
    }
}
