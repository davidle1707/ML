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
    public class MSCategoryElement
    {
        public MSCategoryElement()
        {
            //default values
            this.ShowName = true;
        }

        public MSCategoryElement(String elementName)
        {
            //default values
            this.Name = elementName;
            this.ShowName = true;
        }

        [CategoryElementMS]
        public string Name { get; set; }
        [CategoryElementMS]
        public string HoverText { get; set; }
        [CategoryElementMS]
        public bool ShowName { get; set; }
    }
}
