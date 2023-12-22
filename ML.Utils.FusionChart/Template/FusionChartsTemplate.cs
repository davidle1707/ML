using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ML.Utils.FusionChart.Abstract;
using ML.Utils.FusionChart.PropertySet;

namespace ML.Utils.FusionChart.Template
{
    public abstract class FusionChartsTemplateBase
    {
        private int pointer;
        protected List<String> colors;

        public FusionChartsTemplateBase()
        {
            Reset();
        }

        #region [ Property Set ]
        public AnchorPropertySet AnchorPropertySet { get; protected set; }
        public AnimationPropertySet AnimationPropertySet { get; protected set; }
        public AreaPropertySet AreaPropertySet { get; protected set; }
        public AxisTitlesPropertySet AxisTitlesPropertySet { get; protected set; }
        public BackgroundPropertySet BackgroundPropertySet { get; protected set; }
        public BarsNumberFormatPropertySet BarsNumberFormatPropertySet { get; protected set; }
        public BarsVisibilityPropertySet BarsVisibilityPropertySet { get; protected set; }
        public Canvas2DPropertySet Canvas2DPropertySet { get; protected set; }
        public Canvas3DPropertySet Canvas3DPropertySet { get; protected set; }
        public ChartMarginsPropertySet ChartMarginsPropertySet { get; protected set; }
        public ChartNumericalLimitsPropertySet ChartNumericalLimitsPropertySet { get; protected set; }
        public ChartTitlesPropertySet ChartTitlesPropertySet { get; protected set; }
        public FontBasePropertySet FontBasePropertySet { get; protected set; }
        public FontOutCanvasPropertySet FontOutCanvasPropertySet { get; protected set; }
        public GenericMSVisibilityPropertySet GenericMSVisibilityPropertySet { get; protected set; }
        public GenericVisibilityPropertySet GenericVisibilityPropertySet { get; protected set; }
        public HDivisionalLines_Bar2DPropertySet HDivisionalLines_Bar2DPropertySet { get; protected set; }
        public HDivisionalLinesPropertySet HDivisionalLinesPropertySet { get; protected set; }
        public HoverCaptionPropertySet HoverCaptionPropertySet { get; protected set; }
        public LinePropertySet LinePropertySet { get; protected set; }
        public LineShadowPropertySet LineShadowPropertySet { get; protected set; }
        public NameValueDisplayDistanceControlPropertySet NameValueDisplayDistanceControlPropertySet { get; protected set; }
        public NumberFormatPropertySet NumberFormatPropertySet { get; protected set; }
        public PiePropertySet PiePropertySet { get; protected set; }
        public PieShadowPropertySet PieShadowPropertySet { get; protected set; }
        public PieVisibilityPropertySet PieVisibilityPropertySet { get; protected set; }
        public VDivisionalLines_Bar2DPropertySet VDivisionalLines_Bar2DPropertySet { get; protected set; }
        public VDivisionalLinesPropertySet VDivisionalLinesPropertySet { get; protected set; }
        public ZeroPlane2DPropertySet ZzeroPlane2DPropertySet { get; protected set; }
        public ZeroPlane3DPropertySet ZeroPlane3DPropertySet { get; protected set; }
        #endregion

        #region [ Metodos Publicos ]

        public void Reset()
        {
            pointer = -1;
            colors = null;
        }

        public String GetSeriesColor()
        {
            if (colors == null)
                SetSeriesColors();

            if (pointer == colors.Count - 1)
                pointer = 0;
            else
                pointer++;

            return colors[pointer];
        }

        #endregion

        #region [ Metodos Abstratos ]

        public abstract void SetSeriesColors();

        #endregion
    }
}
