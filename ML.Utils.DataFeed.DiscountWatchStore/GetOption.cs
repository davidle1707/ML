using System;
using System.Threading.Tasks;

namespace ML.Utils.DataFeed.DiscountWatchStore
{
    [Serializable]
    public class GetOption
    {
        public int StartPage { get; set; } = 1;

        public int StartRecordIndex { get; set; } = 0;

        public bool ProductGetFullDetails { get; set; } = true;

        public Func<Product, Task> ProductCallBackAsync { get; set; }

        public int? DelayMillisecondsEachPage { get; set; } = 5000;

        public int? DelayMillisecondsEachProduct { get; set; }
        
        public static GetOption Default => new GetOption();
    }
}
