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
    public sealed class StackedColumn3DChart : MultiSeriesChart
    {
        public StackedColumn3DChart()
        {
            this.AxisTitles = new AxisTitlesPropertySet();
            this.Canvas3D = new Canvas3DPropertySet();
            this.ChartNumericalLimits = new ChartNumericalLimitsPropertySet();
            this.GenericMSVisibility = new GenericMSVisibilityPropertySet();
            this.BarsVisibility = new BarsVisibilityPropertySet();
            this.FontBase = new FontBasePropertySet();
            this.FontOutCanvas = new FontOutCanvasPropertySet();
            this.NumberFormat = new NumberFormatPropertySet();
            this.BarsNumberFormat = new BarsNumberFormatPropertySet();
            this.ZeroPlane3D = new ZeroPlane3DPropertySet();
            this.HDivisionalLines = new HDivisionalLinesPropertySet();
            this.HoverCaption = new HoverCaptionPropertySet();
            this.ChartMargins = new ChartMarginsPropertySet();
        }

        public AxisTitlesPropertySet AxisTitles { get; set; }
        public Canvas3DPropertySet Canvas3D { get; set; }
        public ChartNumericalLimitsPropertySet ChartNumericalLimits { get; set; }
        public GenericMSVisibilityPropertySet GenericMSVisibility { get; set; }
        public BarsVisibilityPropertySet BarsVisibility { get; set; }
        public FontBasePropertySet FontBase { get; set; }
        public FontOutCanvasPropertySet FontOutCanvas { get; set; }
        public NumberFormatPropertySet NumberFormat { get; set; }
        public BarsNumberFormatPropertySet BarsNumberFormat { get; set; }
        public ZeroPlane3DPropertySet ZeroPlane3D { get; set; }
        public HDivisionalLinesPropertySet HDivisionalLines { get; set; }
        public HoverCaptionPropertySet HoverCaption { get; set; }
        public ChartMarginsPropertySet ChartMargins { get; set; }
    }
}
