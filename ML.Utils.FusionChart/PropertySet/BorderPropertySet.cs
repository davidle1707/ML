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
    public class BorderPropertySet : PropertySetBase
    {
        #region [ Construtor ]
        public BorderPropertySet()
        {
            
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private bool? showBorder;
        [GraphElement]
        private string borderColor;
        [GraphElement]
        private int? borderAlpha;
        [GraphElement]
        private int? borderThickness;
        #endregion

        #region [ Propriedades Publicas ]
        /// <summary>
        /// This attribute sets whether a border would be shown around the funnel segments or not. Default value: false
        /// </summary>
        public bool ShowBorder
        {
            get
            {
                if (showBorder == null)
                    throw new NullReferenceException("A propriedade showBorder não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showBorder);
            }
            set
            {
                showBorder = value;
            }
        }

        /// <summary>
        /// 'Hex Color': This attribute sets the color of the border, which is displayed around the funnel segments when showBorder is set as true. Default value: By default, the border color of each funnel segment is the same as their background color.
        /// </summary>
        public string BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        /// <summary>
        /// 'Numerical Value': This attribute sets the thickness, in pixels, of the border, which is displayed around the funnel segments when showBorder is set as 1. Default value: 1
        /// </summary>
        public int BorderThickness
        {
            get
            {
                if (borderThickness == null)
                    throw new NullReferenceException("A propriedade borderThickness não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(borderThickness);
            }
            set
            {
                borderThickness = value;
            }
        }

        /// <summary>
        /// 'value(0-100)': This attribute sets the alpha of the border, which is displayed around the color range when showBorder is set as 1. Default value: 100
        /// </summary>
        public int BorderAlpha
        {
            get
            {
                if (borderAlpha == null)
                    throw new NullReferenceException("A propriedade borderAlpha não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(borderAlpha);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade borderAlpha deve ser um valor inteiro entre 0 e 100.");

                borderAlpha = value;
            }
        }
        #endregion
    }
}
