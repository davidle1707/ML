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
    public sealed class MSColumn2DLineDYChart : MultiSeriesChart, ICombinationChart
    {
        public MSColumn2DLineDYChart()
        {
            this.AxisTitles = new AxisTitlesPropertySet();
            this.Canvas2D = new Canvas2DPropertySet();
            this.ChartNumericalLimits = new ChartNumericalLimitsPropertySet();
            this.GenericMSVisibility = new GenericMSVisibilityPropertySet();
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
        }

        public AxisTitlesPropertySet AxisTitles { get; set; }
        public Canvas2DPropertySet Canvas2D { get; set; }
        public ChartNumericalLimitsPropertySet ChartNumericalLimits { get; set; }
        public GenericMSVisibilityPropertySet GenericMSVisibility { get; set; }
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

        private Dictionary<String, CombinationAxisType> dataSetType;
        public void SetAxisType(String dataSetName, CombinationAxisType axisType)
        {
            if (this.dataSetType == null)
                this.dataSetType = new Dictionary<String, CombinationAxisType>();

            if (this.dataSetType.Keys.Contains(dataSetName))
                this.dataSetType[dataSetName] = axisType;
            else
                this.dataSetType.Add(dataSetName, axisType);

        }

        public Dictionary<String, CombinationAxisType> GetAxisTypeDictionary()
        {
            return this.dataSetType;
        }
    }
}
