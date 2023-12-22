using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML.Utils.FusionChart.PropertySet;

namespace ML.Utils.FusionChart.Template
{
    // To create a template, inherit from ML.Utils.FusionChart.Template.ParadigmaChartTemplate and implement:
    //  - A constructor to define the PropertySets.
    //  - A method to set the series colors. 

    public class OfficeTemplate : FusionChartsTemplateBase
    {
        public OfficeTemplate()
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
            colors.Add("95B3D7");
            colors.Add("D99795");
            colors.Add("C2D69A");
            colors.Add("948B54");
            colors.Add("B2A1C7");
            colors.Add("93CDDD");
            colors.Add("FAC090");
            colors.Add("538ED5");
            colors.Add("A5A5A5");
        }
    }
}
