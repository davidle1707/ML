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
    
    public class SSSetElement
    {
        public SSSetElement()
        {
            //Default values
            this.Value = 0;
            this.ShowName = true;
        }

        [SetElementSS]
        public string Name { get; set; }
        [SetElementSS]
        public decimal Value { get; set; }
        [SetElementSS]
        public string Color { get; set; }
        [SetElementSS]
        public string HoverText { get; set; }
        [SetElementSS]
        public string Link { get; set; }
        [SetElementSS]
        public bool ShowName { get; set; }
    }
}
