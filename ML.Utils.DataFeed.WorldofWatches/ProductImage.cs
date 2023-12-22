using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Utils.DataFeed.WorldofWatches
{
    [Serializable]
    public class ProductImage
    {
        public string Url { get; set; }

        public string FileName { get; set; }

        public byte[] Contents { get; set; }
    }
}
