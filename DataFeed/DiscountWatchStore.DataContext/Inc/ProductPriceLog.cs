using System;

namespace DiscountWatchStore.DataContext.Inc
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
