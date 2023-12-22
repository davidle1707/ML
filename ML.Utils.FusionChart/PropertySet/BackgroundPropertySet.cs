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
using ML.Utils.FusionChart.Abstract;

namespace ML.Utils.FusionChart.PropertySet
{
    public class BackgroundPropertySet : PropertySetBase
    {
        #region [ Contrutor ]
        public BackgroundPropertySet()
        {
            bgAlpha = null;
            bgColor = null;
            bgSWF = null;
        }
        #endregion

        #region [ Campos Privadas ]
        [GraphElement]
        private int? bgAlpha;
        [GraphElement]
        private string bgColor;
        [GraphElement]
        private string bgSWF;
        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// "HexColorCode" : This attribute sets the background color for the chart. You can set any hex color code as the value of this attribute. Remember that you DO NOT need to assign a "#" at the beginning of the hex color code. In fact, whenever you need to provide any hex color code in FusionCharts XML data document, you do not have to assign the # at the beginning.
        /// </summary>
        public string BgColor
        {
            get { return bgColor; }
            set { bgColor = value; }
        }

        /// <summary>
        /// "NumericalValue(0-100)" : This attribute helps you set the alpha (transparency) of the graph. This is particularly useful when you need to load the chart in one of your Flash movies or when you want to set a background image (.swf) for the chart.
        /// </summary>
        public int BgAlpha
        {
            get
            {
                if (bgAlpha == null)
                    throw new NullReferenceException("A propriedade BgAlpha não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(bgAlpha);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade BgAlpha deve ser um valor inteiro entre 0 e 100.");
                bgAlpha = value;
            }
        }

        /// <summary>
        /// "Path of SWF File" : This attribute helps you load an external .swf file as a background for the chart.
        /// </summary>
        public string BgSWF
        {
            get { return bgSWF; }
            set { bgSWF = value; }
        }
        #endregion
    }
}
