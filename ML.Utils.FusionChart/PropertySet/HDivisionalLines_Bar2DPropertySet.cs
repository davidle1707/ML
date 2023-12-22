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
    public class HDivisionalLines_Bar2DPropertySet : PropertySetBase
    {

        #region [ Construtor ]
        public HDivisionalLines_Bar2DPropertySet()
        {
            numHDivLines = null;
            hDivlinecolor = null;
            hDivLineThickness = null;
            hDivLineAlpha = null;
            showAlternateHGridColor = null;
            alternateHGridColor = null;
            alternateHGridAlpha = null;
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private int? numHDivLines;
        [GraphElement]
        private string hDivlinecolor;
        [GraphElement]
        private int? hDivLineThickness;
        [GraphElement]
        private int? hDivLineAlpha;
        [GraphElement]
        private bool? showAlternateHGridColor;
        [GraphElement]
        private string alternateHGridColor;
        [GraphElement]
        private int? alternateHGridAlpha;
        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// "NumericalValue" : This attribute sets the number of divisional lines to be drawn.
        /// </summary>
        public int NumHDivLines
        {
            get
            {
                if (numHDivLines == null)
                    throw new NullReferenceException("A propriedade numHDivLines não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(numHDivLines);
            }
            set
            {
                numHDivLines = value;
            }
        }

        /// <summary>
        /// "HexColorCode" : The color of grid divisional line.
        /// </summary>
        public string HDivLineColor
        {
            get { return hDivlinecolor; }
            set { hDivlinecolor = value; }
        }

        /// <summary>
        /// "NumericalValue" : Thickness (in pixels) of the grid divisional line.
        /// </summary>
        public int HDivLineThickness
        {
            get
            {
                if (hDivLineThickness == null)
                    throw new NullReferenceException("A propriedade hDivLineThickness não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(hDivLineThickness);
            }
            set
            {
                hDivLineThickness = value;
            }
        }

        /// <summary>
        /// "NumericalValue0-100" : Alpha (transparency) of the grid divisional line.
        /// </summary>
        public int HDivLineAlpha
        {
            get
            {
                if (hDivLineAlpha == null)
                    throw new NullReferenceException("A propriedade hDivLineAlpha não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(hDivLineAlpha);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade anchorBgAlpha deve ser um valor inteiro entre 0 e 100.");

                hDivLineAlpha = value;
            }
        }

        /// <summary>
        ///  Option to show/hide the textual value of the divisional line.
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
        /// Option on whether to show alternate colored horizontal grid bands.
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
