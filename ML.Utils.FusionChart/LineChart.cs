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
    public sealed class LineChart : SingleSeriesChart
    {
        public LineChart()
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
            this.HDivisionalLines = new HDivisionalLinesPropertySet();
            this.VDivisionalLines = new VDivisionalLinesPropertySet();
            this.HoverCaption = new HoverCaptionPropertySet();
            this.ChartMargins = new ChartMarginsPropertySet();
            this.LinePropertySet = new LinePropertySet();
            this.LineShadow = new LineShadowPropertySet();
            this.Anchor = new AnchorPropertySet();
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
        public HDivisionalLinesPropertySet HDivisionalLines { get; set; }
        public VDivisionalLinesPropertySet VDivisionalLines { get; set; }
        public HoverCaptionPropertySet HoverCaption { get; set; }
        public ChartMarginsPropertySet ChartMargins { get; set; }
        public LinePropertySet LinePropertySet { get; set; }
        public LineShadowPropertySet LineShadow { get; set; }
        public AnchorPropertySet Anchor { get; set; }

    }
}
