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
    public class LinePropertySet : PropertySetBase
    {

        #region [ Construtor ]
        public LinePropertySet()
        {
            lineColor = null;
            lineThickness = null;
            lineAlpha = null;
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private string lineColor;
        [GraphElement]
        private int? lineThickness;
        [GraphElement]
        private int? lineAlpha;
        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// "Hex Code" : If you want the entire line chart to be plotted in one color, set that color for this attribute.
        /// </summary>
        public string LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
        }

        /// <summary>
        /// "Numeric Value" : Thickness of the line (in pixels).
        /// </summary>
        public int LineThickness
        {
            get
            {
                if (lineThickness == null)
                    throw new NullReferenceException("A propriedade lineThickness não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(lineThickness);
            }
            set
            {
                lineThickness = value;
            }
        }

        /// <summary>
        /// "0-100" : Transparency of the line.
        /// </summary>
        public int LineAlpha
        {
            get
            {
                if (lineAlpha == null)
                    throw new NullReferenceException("A propriedade lineAlpha não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(lineAlpha);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade anchorBgAlpha deve ser um valor inteiro entre 0 e 100.");

                lineAlpha = value;
            }
        }
        #endregion
    }
}
