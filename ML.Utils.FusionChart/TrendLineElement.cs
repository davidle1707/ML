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
using ML.Utils.FusionChart.Attribute;

namespace ML.Utils.FusionChart
{
    public class TrendLineElement
    {
        public TrendLineElement()
        {
            this.IsTrendZone = true;
            this.ShowOnTop = true;
            this.Alpha = 50;
            this.Thickness = 2;
        }

        /// <summary>
        /// 'NumericalValue': The starting y-axis value for the trendline. Say, if you want to plot a slanted trendline from value 102 to 109, the startValue would 102.
        /// </summary>
        [TrendLineElement]
        public decimal StartValue { get; set; }

        /// <summary>
        /// 'NumericalValue': The ending y-axis value for the trendline. Say, if you want to plot a slanted trendline from value 102 to 109, the endValue would 109. If you do not specify a value for endValue, it would automatically assume the same value as startValue.
        /// </summary>
        [TrendLineElement]
        public decimal EndValue { get; set; }

        /// <summary>
        /// 'HexCode' : Color of the trend line and its associated text.
        /// </summary>
        [TrendLineElement]
        public string Color { get; set; }

        /// <summary>
        /// 'StringValue' : If you want to display a string caption for the trend line by its side, you can use this attribute. Example: displayValue='Last Month High'. When you don't supply this attribute, it automatically takes the value of startValue.
        /// </summary>
        [TrendLineElement]
        public string DisplayValue { get; set; }
        
        /// <summary>
        /// 'NumericalValue' : Thickness of the trend line
        /// </summary>
        [TrendLineElement]
        public float Thickness { get; set; }

        /// <summary>
        /// '1/0': Whether the trend would display a line, or a zone (filled colored rectangle).
        /// </summary>
        [TrendLineElement]
        public bool IsTrendZone { get; set; }

        /// <summary>
        /// '1/0': Whether the trend line/zone would be displayed over other elements of the chart.
        /// </summary>
        [TrendLineElement]
        public bool ShowOnTop { get; set; }

        /// <summary>
        /// 'NumericalValue0-100': Alpha (transparency) of the trend line
        /// </summary>
        [TrendLineElement]
        public int Alpha { get; set; }
    }
}
