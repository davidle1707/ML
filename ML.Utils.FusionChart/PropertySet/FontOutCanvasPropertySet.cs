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
    public class FontOutCanvasPropertySet : PropertySetBase
    {
        #region [ Cosntrutor ]
        public FontOutCanvasPropertySet()
        {
            outCnvBaseFont = null;
            outCnvBaseFontSze = null;
            outCnvBaseFontColor = null;
        }
        #endregion

        #region [ Propriedades Privadas ]

        [GraphElement]
        private string outCnvBaseFont;
        [GraphElement]
        private string outCnvBaseFontSze;
        [GraphElement]
        private string outCnvBaseFontColor;

        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// "FontName" : This attribute sets the base font family of the chart font which lies outside the canvas i.e., all the values and the names in the chart which lie outside the canvas will be displayed using the font name provided here.
        /// </summary>
        public string OutCnvBaseFont
        {
            get
            {
                return outCnvBaseFont;
            }
            set
            {
                outCnvBaseFont = value;
            }
        }

        /// <summary>
        /// "FontSize" : This attribute sets the base font size of the chart i.e., all the values and the names in the chart which lie outside the canvas will be displayed using the font size provided here.
        /// </summary>
        public string OutCnvBaseFontSze
        {
            get
            {
                return outCnvBaseFontSze;
            }
            set
            {
                outCnvBaseFontSze = value;
            }
        }

        /// <summary>
        /// "HexColorCode": This attribute sets the base font color of the chart i.e., all the values and the names in the chart which lie outside the canvas will be displayed using the font color provided here.
        /// </summary>
        public string OutCnvBaseFontColor
        {
            get
            {
                return outCnvBaseFontColor;
            }
            set
            {
                outCnvBaseFontColor = value;
            }
        }

        #endregion
    }
}
