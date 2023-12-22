/**
 * 
 *    Libero API for Fusion Charts to Asp.Net (Webforms & MVC)
 *    by: Roberto Barbedo (November, 2010) 
 *    email: r_barbedo@yahoo.com.br
 * 
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML.Utils.FusionChart.Attribute;

namespace ML.Utils.FusionChart
{
    public class MSDataSet
    {
        public MSDataSet()
        {
            //default values
            this.SetElementSet = new List<MSSetElement>();
            this.Alpha = 100;
            this.ShowValues = true;
        }

        public MSDataSet(String seriesName)
        {
            //default values
            this.SetElementSet = new List<MSSetElement>();
            this.Alpha = 100;
            this.ShowValues = true;
            this.SeriesName = seriesName;
        }

        public MSDataSet(String seriesName, String color)
        {
            //default values
            this.SetElementSet = new List<MSSetElement>();
            this.Alpha = 100;
            this.ShowValues = true;
            this.SeriesName = seriesName;
            this.Color = color;
        }

        [DatasetMS]
        public string SeriesName { get; set; }
        [DatasetMS]
        public string Color { get; set; }
        [DatasetMS]
        public bool ShowValues { get; set; }
        [DatasetMS]
        public int Alpha { get; set; }

        /// <summary>
        /// Only applied to Combination Charts. Accepted values: P for primary, S for secundary.
        /// </summary>
        [DatasetMS]
        public string ParentYAxis { get; set; } //only for combination types

        public List<MSSetElement> SetElementSet { get; set; }
    }
}
