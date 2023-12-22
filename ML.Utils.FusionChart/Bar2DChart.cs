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
using ML.Utils.FusionChart.Attribute;

namespace ML.Utils.FusionChart
{
    [Graph]
    public sealed class Bar2DChart : SingleSeriesChart
    {
        public Bar2DChart()
        {
            this.AxisTitles = new AxisTitlesPropertySet();
            this.Canvas2D = new Canvas2DPropertySet();
            this.ChartNumericalLimits = new ChartNumericalLimitsPropertySet();
            this.GenericVisibility = new GenericVisibilityPropertySet();
            this.BarsVisibility = new BarsVisibilityPropertySet();
            this.FontBase = new FontBasePropertySet();
            this.FontOutCanvas = new FontOutCanvasPropertySet();
            this.NumberFormat = new NumberFormatPropertySet();
            this.BarsNumberFormat = new BarsNumberFormatPropertySet();
            this.ZeroPlane2D = new ZeroPlane2DPropertySet();
            this.HDivisionalLines = new HDivisionalLines_Bar2DPropertySet();
            this.VDivisionalLines = new VDivisionalLines_Bar2DPropertySet();
            this.HoverCaption = new HoverCaptionPropertySet();
            this.ChartMargins = new ChartMarginsPropertySet();
        }

        public AxisTitlesPropertySet AxisTitles { get; set; }
        public Canvas2DPropertySet Canvas2D { get; set; }
        public ChartNumericalLimitsPropertySet ChartNumericalLimits { get; set; }
        public GenericVisibilityPropertySet GenericVisibility { get; set; }
        public BarsVisibilityPropertySet BarsVisibility { get; set; }
        public FontBasePropertySet FontBase { get; set; }
        public FontOutCanvasPropertySet FontOutCanvas { get; set; }
        public NumberFormatPropertySet NumberFormat { get; set; }
        public BarsNumberFormatPropertySet BarsNumberFormat { get; set; }
        public ZeroPlane2DPropertySet ZeroPlane2D { get; set; }
        public HDivisionalLines_Bar2DPropertySet HDivisionalLines { get; set; }
        public VDivisionalLines_Bar2DPropertySet VDivisionalLines { get; set; }
        public HoverCaptionPropertySet HoverCaption { get; set; }
        public ChartMarginsPropertySet ChartMargins { get; set; }

    }
}
