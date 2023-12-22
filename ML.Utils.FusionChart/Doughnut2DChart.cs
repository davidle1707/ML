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
    public sealed class Doughnut2DChart : SingleSeriesChart
    {
        public Doughnut2DChart()
        {
            this.GenericVisibility = new GenericVisibilityPropertySet();
            this.PieVisibility = new PieVisibilityPropertySet();
            this.FontBase = new FontBasePropertySet();
            this.NumberFormat = new NumberFormatPropertySet();
            this.HoverCaption = new HoverCaptionPropertySet();
            this.PiePropertySet = new PiePropertySet();
            this.NameValueDisplayDistance = new NameValueDisplayDistanceControlPropertySet();
            this.PieShadow = new PieShadowPropertySet();
        }

        public GenericVisibilityPropertySet GenericVisibility { get; set; }
        public PieVisibilityPropertySet PieVisibility { get; set; }
        public FontBasePropertySet FontBase { get; set; }
        public NumberFormatPropertySet NumberFormat { get; set; }
        public HoverCaptionPropertySet HoverCaption { get; set; }
        public PiePropertySet PiePropertySet { get; set; }
        public PieShadowPropertySet PieShadow { get; set; }
        public NameValueDisplayDistanceControlPropertySet NameValueDisplayDistance { get; set; }
    }
}
