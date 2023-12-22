using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML.Utils.FusionChart.PropertySet;

namespace ML.Utils.FusionChart.Template
{
    public class OceanTemplate : FusionChartsTemplateBase
    {
        public OceanTemplate()
            : base()
        {
            //Set here the PropertySets for this template.

            //Exemple:    
            //base.FontBasePropertySet = new FontBasePropertySet()
            //{
            //    BaseFont = "Verdana"
            //};
        }

        public override void SetSeriesColors()
        {
            //Set here the series colors for the template.

            colors = new List<string>();
            colors.Add("3366aa");
            colors.Add("6699aa");
            colors.Add("aabbbb");
            colors.Add("778877");
            colors.Add("334433");
        }
    }
}
