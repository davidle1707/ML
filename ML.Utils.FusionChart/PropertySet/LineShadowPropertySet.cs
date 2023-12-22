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
    public class LineShadowPropertySet : PropertySetBase
    {

        #region [ Construtor ]
        public LineShadowPropertySet()
        {
            showShadow = null;
            shadowColor = null;
            shadowThickness = null;
            shadowAlpha = null;
            shadowXShift = null;
            shadowYShift = null;
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private bool? showShadow;
        [GraphElement]
        private string shadowColor;
        [GraphElement]
        private int? shadowThickness;
        [GraphElement]
        private int? shadowAlpha;
        [GraphElement]
        private int? shadowXShift;
        [GraphElement]
        private int? shadowYShift;
        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// This attribute helps you set whether the line shadow would be shown or not.
        /// </summary>
        public bool ShowShadow
        {
            get
            {
                if (showShadow == null)
                    throw new NullReferenceException("A propriedade showShadow não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showShadow);
            }
            set
            {
                showShadow = value;
            }
        }

        /// <summary>
        /// "Hex Code" : If you want to set your own shadow color, you'll need to specify that color for this attribute.
        /// </summary>
        public string ShadowColor
        {
            get { return shadowColor; }
            set { shadowColor = value; }
        }

        /// <summary>
        /// "Numeric Value" : This attribute helps you set the thickness of the shadow line (in pixels).
        /// </summary>
        public int ShadowThickness
        {
            get
            {
                if (shadowThickness == null)
                    throw new NullReferenceException("A propriedade shadowThickness não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(shadowThickness);
            }
            set
            {
                shadowThickness = value;
            }
        }

        /// <summary>
        /// "0-100" : This attribute sets the transparency of the shadow line.
        /// </summary>
        public int ShadowAlpha
        {
            get
            {
                if (shadowAlpha == null)
                    throw new NullReferenceException("A propriedade shadowAlpha não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(shadowAlpha);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade anchorBgAlpha deve ser um valor inteiro entre 0 e 100.");

                shadowAlpha = value;
            }
        }

        /// <summary>
        /// "Numeric Value" : This attribute helps you set the x shift of the shadow line from the chart line. That is, if you want to show the shadow 3 pixel right from the actual line, set this attribute to 3. Similarly, if you want the shadow to appear on the left of the actual line, set it to -3.
        /// </summary>
        public int ShadowXShift
        {
            get
            {
                if (shadowXShift == null)
                    throw new NullReferenceException("A propriedade shadowXShift não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(shadowXShift);
            }
            set
            {
                shadowXShift = value;
            }
        }


        /// <summary>
        /// "Numeric Value" : This attribute helps you set the y shift of the shadow line from the chart line. That is, if you want to show the shadow 3 pixel below the actual line, set this attribute to 3. Similarly, if you want the shadow to appear above the actual line, set it to -3.
        /// </summary>
        public int ShadowYShift
        {
            get
            {
                if (shadowYShift == null)
                    throw new NullReferenceException("A propriedade shadowYShift não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(shadowYShift);
            }
            set
            {
                shadowYShift = value;
            }
        }
        #endregion
    }
}
