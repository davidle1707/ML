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
    public class ZeroPlane2DPropertySet : PropertySetBase
    {

        #region [ Construtor ]
        public ZeroPlane2DPropertySet()
        {
            zeroPlaneThickness = null;
            zeroPlaneColor = null;
            zeroPlaneAlpha = null;
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private int? zeroPlaneThickness;
        [GraphElement]
        private string zeroPlaneColor;
        [GraphElement]
        private int? zeroPlaneAlpha;
        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// "Numeric Value" : Thickness (in pixels) of the line indicating the zero plane.
        /// </summary>
        public int ZeroPlaneThickness
        {
            get
            {
                if (zeroPlaneThickness == null)
                    throw new NullReferenceException("A propriedade zeroPlaneThickness não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(zeroPlaneThickness);
            }
            set
            {
                zeroPlaneThickness = value;
            }
        }

        /// <summary>
        /// "Hex Code" : The intended color for the zero plane.
        /// </summary>
        public string ZeroPlaneColor
        {
            get { return zeroPlaneColor; }
            set { zeroPlaneColor = value; }
        }

        /// <summary>
        /// "Numerical Value 0-100" : The intended transparency for the zero plane.
        /// </summary>
        public int ZeroPlaneAlpha
        {
            get
            {
                if (zeroPlaneAlpha == null)
                    throw new NullReferenceException("A propriedade zeroPlaneAlpha não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(zeroPlaneAlpha);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade anchorBgAlpha deve ser um valor inteiro entre 0 e 100.");

                zeroPlaneAlpha = value;
            }
        }
        #endregion
    }
}
