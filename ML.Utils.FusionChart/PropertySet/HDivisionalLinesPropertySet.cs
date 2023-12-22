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
    public class HDivisionalLinesPropertySet : PropertySetBase
    {

        #region [ Construtor ]
        public HDivisionalLinesPropertySet()
        {
            numDivLines = null;
            divLineColor = null;
            divLineThickness = null;
            divLineAlpha = null;
            showDivLineValue = null;
            showAlternateHGridColor = null;
            alternateHGridColor = null;
            alternateHGridAlpha = null;
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private int? numDivLines;
        [GraphElement]
        private string divLineColor;
        [GraphElement]
        private int? divLineThickness;
        [GraphElement]
        private int? divLineAlpha;
        [GraphElement]
        private bool? showDivLineValue;
        [GraphElement]
        private bool? showAlternateHGridColor;
        [GraphElement]
        private string alternateHGridColor;
        [GraphElement]
        private int? alternateHGridAlpha;
        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// This attribute sets the number of divisional lines to be drawn.
        /// </summary>
        public int NumDivLines
        {
            get
            {
                if (numDivLines == null)
                    throw new NullReferenceException("A propriedade numDivLines não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(numDivLines);
            }
            set
            {
                numDivLines = value;
            }
        }

        /// <summary>
        ///  The color of grid divisional line.
        /// </summary>
        public string DivLineColor
        {
            get { return divLineColor; }
            set { divLineColor = value; }
        }

        /// <summary>
        ///  Thickness (in pixels) of the grid divisional line.
        /// </summary>
        public int DivLineThickness
        {
            get
            {
                if (divLineThickness == null)
                    throw new NullReferenceException("A propriedade divLineThickness não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(divLineThickness);
            }
            set
            {
                divLineThickness = value;
            }
        }

        /// <summary>
        ///  Alpha (transparency) of the grid divisional line.
        /// </summary>
        public int DivLineAlpha
        {
            get
            {
                if (divLineAlpha == null)
                    throw new NullReferenceException("A propriedade divLineAlpha não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(divLineAlpha);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade anchorBgAlpha deve ser um valor inteiro entre 0 e 100.");

                divLineAlpha = value;
            }
        }

        /// <summary>
        ///  Option to show/hide the textual value of the divisional line.
        /// </summary>
        public bool ShowDivLineValue
        {
            get
            {
                if (showDivLineValue == null)
                    throw new NullReferenceException("A propriedade showDivLineValue não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showDivLineValue);
            }
            set
            {
                showDivLineValue = value;
            }
        }

        /// <summary>
        ///  Option on whether to show alternate colored horizontal grid bands.
        /// </summary>
        public bool ShowAlternateHGridColor
        {
            get
            {
                if (showAlternateHGridColor == null)
                    throw new NullReferenceException("A propriedade showAlternateHGridColor não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showAlternateHGridColor);
            }
            set
            {
                showAlternateHGridColor = value;
            }
        }

        /// <summary>
        /// "HexColorCode" : Color of the alternate horizontal grid bands.
        /// </summary>
        public string AlternateHGridColor
        {
            get { return alternateHGridColor; }
            set { alternateHGridColor = value; }
        }

        /// <summary>
        /// "NumericalValue0-100" : Alpha (transparency) of the alternate horizontal grid bands.
        /// </summary>
        public int AlternateHGridAlpha
        {
            get
            {
                if (alternateHGridAlpha == null)
                    throw new NullReferenceException("A propriedade alternateHGridAlpha não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(alternateHGridAlpha);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade anchorBgAlpha deve ser um valor inteiro entre 0 e 100.");

                alternateHGridAlpha = value;
            }
        }
        #endregion
    }
}
