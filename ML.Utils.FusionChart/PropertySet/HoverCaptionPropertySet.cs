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
    public class HoverCaptionPropertySet : PropertySetBase
    {

        #region [ Construtor ]
        public HoverCaptionPropertySet()
        {
            showHoverCap = null;
            hoverCapBgColor = null;
            hoverCapBorderColor = null;
            hoverCapSepChar = null;
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private bool? showHoverCap;
        [GraphElement]
        private string hoverCapBgColor;
        [GraphElement]
        private string hoverCapBorderColor;
        [GraphElement]
        private string hoverCapSepChar;
        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        ///  Option whether to show/hide hover caption box.
        /// </summary>
        public bool ShowHoverCap
        {
            get
            {
                if (showHoverCap == null)
                    throw new NullReferenceException("A propriedade showHoverCap não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showHoverCap);
            }
            set
            {
                showHoverCap = value;
            }
        }

        /// <summary>
        /// "HexColorCode" : Background color of the hover caption box.
        /// </summary>
        public string HoverCapBgColor
        {
            get { return hoverCapBgColor; }
            set { hoverCapBgColor = value; }
        }

        /// <summary>
        /// "HexColorCode" : Border color of the hover caption box.
        /// </summary>
        public string HoverCapBorderColor
        {
            get { return hoverCapBorderColor; }
            set { hoverCapBorderColor = value; }
        }

        /// <summary>
        /// "Char" : The character specified as the value of this attribute separates the name and value displayed in the hover caption box.
        /// </summary>
        public string HoverCapSepChar
        {
            get { return hoverCapSepChar; }
            set { hoverCapSepChar = value; }
        }
        #endregion
    }
}
