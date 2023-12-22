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
    public class MSCategory
    {
        public MSCategory()
        {
            //default values
            this.CategoryElementSet = new List<MSCategoryElement>();
            this.FontSize = 10;
        }

        [CategoryMS]
        public string Font { get; set; }
        [CategoryMS]
        public int FontSize { get; set; }
        [CategoryMS]
        public string FontColor { get; set; }
        
        public List<MSCategoryElement> CategoryElementSet { get; set; }
    }
}
