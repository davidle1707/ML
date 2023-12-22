using System;

namespace WorldofWatches.DataContext.Inc
{
    [Serializable]
    public class ProductPriceLog : ProductPrice
    {
        public ProductPriceLog()
        {
            LoggedDate = DateTime.UtcNow;
        }

        public DateTime LoggedDate { get; set; }
    }
}
