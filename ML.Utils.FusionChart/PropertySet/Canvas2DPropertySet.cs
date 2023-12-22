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
    public class Canvas2DPropertySet : PropertySetBase
    {
        #region [ Construtor ]
        public Canvas2DPropertySet()
        {
            canvasBgColor = null;
            canvasBgAlpha = null;
            canvasBorderColor = null;
            canvasBorderThickness = null;
        }
        #endregion

        #region [ Propriedades Privadas ]

        [GraphElement]
        private string canvasBgColor;
        [GraphElement]
        private int? canvasBgAlpha;
        [GraphElement]
        private string canvasBorderColor;
        [GraphElement]
        private int? canvasBorderThickness;

        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// "HexColorCode" : This attribute helps you set the background color of the canvas.
        /// </summary>
        public string CanvasBgColor
        {
            get
            {
                return canvasBgColor;
            }
            set
            {
                canvasBgColor = value;
            }
        }

        /// <summary>
        /// "NumericalValue(0-100)" : This attribute helps you set the alpha (transparency) of the canvas.
        /// </summary>
        public int CanvasBgAlpha
        {
            get
            {
                if (canvasBgAlpha == null)
                    throw new NullReferenceException("A propriedade CanvasBgAlpha não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(canvasBgAlpha);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade CanvasBgAlpha deve ser um valor inteiro entre 0 e 100.");

                canvasBgAlpha = value;
            }
        }

        /// <summary>
        /// "HexColorCode" : This attribute helps you set the border color of the canvas.
        /// </summary>
        public string CanvasBorderColor
        {
            get
            {
                return canvasBorderColor;
            }
            set
            {
                canvasBorderColor = value;
            }
        }

        /// <summary>
        /// "NumericalValue(0-100)" : This attribute helps you set the border thickness (in pixels) of the canvas.
        /// </summary>
        public int CanvasBorderThickness
        {
            get
            {
                if (canvasBorderThickness == null)
                    throw new NullReferenceException("A propriedade CanvasBorderThickness não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(canvasBorderThickness);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade CanvasBorderThickness deve ser um valor inteiro entre 0 e 100.");

                canvasBorderThickness = value;
            }
        }

        #endregion
    }
}
