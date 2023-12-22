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
    public class AreaPropertySet : PropertySetBase
    {

        #region [ Construtor ]
        public AreaPropertySet()
        {
            showAreaBorder = null;
            areaBorderThickness = null;
            areaBorderColor = null;
            areaBgColor = null;
            areaAlpha = null;
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private bool? showAreaBorder;
        [GraphElement]
        private int? areaBorderThickness;
        [GraphElement]
        private string areaBorderColor;
        [GraphElement]
        private string areaBgColor;
        [GraphElement]
        private int? areaAlpha;
        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        ///  Configuration whether the border over the area would be shown or not.
        /// </summary>
        public bool ShowAreaBorder
        {
            get
            {
                if (showAreaBorder == null)
                    throw new NullReferenceException("A propriedade showAreaBorder não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showAreaBorder);
            }
            set
            {
                showAreaBorder = value;
            }
        }

        /// <summary>
        /// "Numeric Value" : If the area border is to be shown, this attribute sets the thickness (in pixels) of the area border.
        /// </summary>
        public int AreaBorderThickness
        {
            get
            {
                if (areaBorderThickness == null)
                    throw new NullReferenceException("A propriedade areaBorderThickness não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(areaBorderThickness);
            }
            set
            {
                areaBorderThickness = value;
            }
        }

        /// <summary>
        /// "Hex Color" : If the area border is to be shown, this attribute sets the color of the area border.
        /// </summary>
        public string AreaBorderColor
        {
            get { return areaBorderColor; }
            set { areaBorderColor = value; }
        }

        /// <summary>
        /// "Hex Color" : If you want the entire area chart to be filled with one color, set that color for this attribute.
        /// </summary>
        public string AreaBgColor
        {
            get { return areaBgColor; }
            set { areaBgColor = value; }
        }

        /// <summary>
        /// Transparency of the area fill.
        /// </summary>
        public int AreaAlpha
        {
            get
            {
                if (areaAlpha == null)
                    throw new NullReferenceException("A propriedade areaAlpha não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(areaAlpha);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade areaAlpha deve ser um valor inteiro entre 0 e 100.");

                areaAlpha = value;
            }
        }
        #endregion
    }
}
