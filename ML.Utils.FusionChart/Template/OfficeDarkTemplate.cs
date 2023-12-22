using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML.Utils.FusionChart.PropertySet;

namespace ML.Utils.FusionChart.Template
{
    public class OfficeDarkTemplate : FusionChartsTemplateBase
    {
        public OfficeDarkTemplate()
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
            colors = new List<string>();
            colors.Add("254061");
            colors.Add("953735");
            colors.Add("75923C");
            colors.Add("4A452A");
            colors.Add("60497B");
            colors.Add("31849B");
            colors.Add("E46D0A");
            colors.Add("17375D");
            colors.Add("000000");
        }
    }
}
