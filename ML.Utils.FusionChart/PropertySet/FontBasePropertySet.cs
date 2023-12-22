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
using ML.Utils.FusionChart.Attribute;

namespace ML.Utils.FusionChart.PropertySet
{
    public class FontBasePropertySet : PropertySetBase
    {
        #region [ Cosntrutor ]
        public FontBasePropertySet()
        {
            baseFont = null;
            baseFontSize = null;
            baseFontColor = null;
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private string baseFont;
        [GraphElement]
        private string baseFontSize;
        [GraphElement]
        private string baseFontColor;
        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// "FontName" : This attribute sets the base font family of the chart font which lies on the canvas i.e., all the values and the names in the chart which lie on the canvas will be displayed using the font name provided here.
        /// </summary>
        public string BaseFont
        {
            get
            {
                return baseFont;
            }
            set
            {
                baseFont = value;
            }
        }

        /// <summary>
        /// "FontSize" : This attribute sets the base font size of the chart i.e., all the values and the names in the chart which lie on the canvas will be displayed using the font size provided here.
        /// </summary>
        public string BaseFontSize
        {
            get
            {
                return baseFontSize;
            }
            set
            {
                baseFontSize = value;
            }
        }

        /// <summary>
        /// "HexColorCode" : This attribute sets the base font color of the chart i.e., all the values and the names in the chart which lie on the canvas will be displayed using the font color provided here.
        /// </summary>
        public string BaseFontColor
        {
            get
            {
                return baseFontColor;
            }
            set
            {
                baseFontColor = value;
            }
        }

        #endregion
    }
}
