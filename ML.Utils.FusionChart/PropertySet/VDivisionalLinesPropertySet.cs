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
    public class VDivisionalLinesPropertySet : PropertySetBase
    {

        #region [ Construtor ]
        public VDivisionalLinesPropertySet()
        {
            numVDivLines = null;
            vDivLineColor = null;
            vDivLineThickness = null;
            vDivLineAlpha = null;
            showAlternateVGridColor = null;
            alternateVGridColor = null;
            alternateVGridAlpha = null;
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private int? numVDivLines;
        [GraphElement]
        private string vDivLineColor;
        [GraphElement]
        private int? vDivLineThickness;
        [GraphElement]
        private int? vDivLineAlpha;
        [GraphElement]
        private bool? showAlternateVGridColor;
        [GraphElement]
        private string alternateVGridColor;
        [GraphElement]
        private int? alternateVGridAlpha;
        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// "NumericalValue" : Sets the number of horizontal divisional lines to be drawn.
        /// </summary>
        public int NumVDivLines
        {
            get
            {
                if (numVDivLines == null)
                    throw new NullReferenceException("A propriedade numVDivLines não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(numVDivLines);
            }
            set
            {
                numVDivLines = value;
            }
        }

        /// <summary>
        /// "HexColorCode" : Color of horizontal grid divisional line.
        /// </summary>
        public string VDivLineColor
        {
            get { return vDivLineColor; }
            set { vDivLineColor = value; }
        }

        /// <summary>
        /// "NumericalValue" : Thickness (in pixels) of the line
        /// </summary>
        public int VDivLineThickness
        {
            get
            {
                if (vDivLineThickness == null)
                    throw new NullReferenceException("A propriedade vDivLineThickness não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(vDivLineThickness);
            }
            set
            {
                vDivLineThickness = value;
            }
        }

        /// <summary>
        /// "NumericalValue0-100" : Alpha (transparency) of the line.
        /// </summary>
        public int VDivLineAlpha
        {
            get
            {
                if (vDivLineAlpha == null)
                    throw new NullReferenceException("A propriedade vDivLineAlpha não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(vDivLineAlpha);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade anchorBgAlpha deve ser um valor inteiro entre 0 e 100.");

                vDivLineAlpha = value;
            }
        }

        /// <summary>
        ///  Option on whether to show alternate colored horizontal grid bands.
        /// </summary>
        public bool ShowAlternateVGridColor
        {
            get
            {
                if (showAlternateVGridColor == null)
                    throw new NullReferenceException("A propriedade showAlternateVGridColor não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showAlternateVGridColor);
            }
            set
            {
                showAlternateVGridColor = value;
            }
        }

        /// <summary>
        /// "HexColorCode" : Color of the alternate horizontal grid bands.
        /// </summary>
        public string AlternateVGridColor
        {
            get { return alternateVGridColor; }
            set { alternateVGridColor = value; }
        }

        /// <summary>
        /// "NumericalValue0-100" : Alpha (transparency) of the alternate horizontal grid bands.
        /// </summary>
        public int AlternateVGridAlpha
        {
            get
            {
                if (alternateVGridAlpha == null)
                    throw new NullReferenceException("A propriedade alternateVGridAlpha não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(alternateVGridAlpha);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade anchorBgAlpha deve ser um valor inteiro entre 0 e 100.");

                alternateVGridAlpha = value;
            }
        }
        #endregion
    }
}
