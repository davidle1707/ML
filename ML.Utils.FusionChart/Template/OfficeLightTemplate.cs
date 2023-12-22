using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML.Utils.FusionChart.PropertySet;

namespace ML.Utils.FusionChart.Template
{
    public class OfficeLightTemplate : FusionChartsTemplateBase
    {
        public OfficeLightTemplate()
            : base()
        {
            //Set here the PropertySets for this template.

            base.FontBasePropertySet = new FontBasePropertySet()
            {
                BaseFont = "Verdana"
            };
        }

        public override void SetSeriesColors()
        {
            //Set here the series colors for the template.

            colors = new List<string>();
            colors.Add("B8CCE4");
            colors.Add("E6B9B8");
            colors.Add("D7E4BC");
            colors.Add("C5BE97");
            colors.Add("CCC0DA");
            colors.Add("B6DDE8");
            colors.Add("FCD5B4");
            colors.Add("8DB4E3");
            colors.Add("BFBFBF");
        }
    }
}
