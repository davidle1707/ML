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
using ML.Utils.FusionChart.Abstract;
using ML.Utils.FusionChart.PropertySet;

namespace ML.Utils.FusionChart
{
    [ML.Utils.FusionChart.Attribute.Chart]
    public sealed class FunnelChart: SingleSeriesChart
    {
        public FunnelChart()
        {
            this.GenericVisibility = new GenericVisibilityPropertySet();
            this.FunnelPropertySet = new FunnelPropertySet();
            this.NumberFormat = new NumberFormatPropertySet();
            this.FontBase = new FontBasePropertySet();
            this.HoverCaption = new HoverCaptionPropertySet();
            this.Border = new BorderPropertySet();
            this.ChartMargins = new ChartMarginsPropertySet();
        }

        public GenericVisibilityPropertySet GenericVisibility { get; set; }
        public FunnelPropertySet FunnelPropertySet { get; set; }
        public NumberFormatPropertySet NumberFormat { get; set; }
        public FontBasePropertySet FontBase { get; set; }
        public HoverCaptionPropertySet HoverCaption { get; set; }
        public BorderPropertySet Border { get; set; }
        public ChartMarginsPropertySet ChartMargins { get; set; }
        
    }
}
