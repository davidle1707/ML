using System;
using System.Collections.Generic;

namespace ML.Utils.DataFeed.DiscountWatchStore
{
    [Serializable]
    public class ProductSpecification
    {
        public ProductSpecification()
        {
            Features = new List<string>();

            Calendars = new List<string>();

            DialColors = new List<string>();

            CaseMaterials = new List<string>();

            BandMaterials = new List<string>();
        }

        public string Gender { get; set; }

        /// <summary>
        /// separate: ,
        /// </summary>
        public List<string> CaseMaterials { get; set; }

        public string CaseWidth { get; set; }

        public string CaseHeight { get; set; }

        public string CaseThickness { get; set; }

        public string Movement { get; set; }

        /// <summary>
        /// separate: ,
        /// </summary>
        public List<string> Calendars { get; set; }

        /// <summary>
        /// separate: ,
        /// </summary>
        public List<string> Features { get; set; }

        /// <summary>
        /// separate: ||
        /// </summary>
        public List<string> DialColors { get; set; }

        public string WaterResistant { get; set; }

        public string Crown { get; set; }

        public string Crystal { get; set; }

        /// <summary>
        /// separate: ,
        /// </summary>
        public List<string> BandMaterials { get; set; }

        public string BandWidth { get; set; }

        public string ClaspType { get; set; }
    }
}
