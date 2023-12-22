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
    public class PiePropertySet : PropertySetBase
    {

        #region [ Construtor ]
        public PiePropertySet()
        {
            pieRadius = null;
            pieSliceDepth = null;
            pieYScale = null;
            pieBorderAlpha = null;
            pieFillAlpha = null;
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private int? pieRadius;
        [GraphElement]
        private int? pieSliceDepth;
        [GraphElement]
        private int? pieYScale;
        [GraphElement]
        private int? pieBorderAlpha;
        [GraphElement]
        private int? pieFillAlpha;
        [GraphElement]
        private int? pieBorderThickness;
        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// "Numeric Pixels": FusionCharts automatically calculates the best fit pie radius for the chart. However, if you want to enforce one of your own radius values, you can set it using this attribute.
        /// </summary>
        public int PieRadius
        {
            get
            {
                if (pieRadius == null)
                    throw new NullReferenceException("A propriedade pieRadius não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(pieRadius);
            }
            set
            {
                pieRadius = value;
            }
        }

        /// <summary>
        /// "Numeric Value" : This attribute helps you set the 3D height (depth) of the pies on the chart (in pixels).
        /// </summary>
        public int PieSliceDepth
        {
            get
            {
                if (pieSliceDepth == null)
                    throw new NullReferenceException("A propriedade pieSliceDepth não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(pieSliceDepth);
            }
            set
            {
                pieSliceDepth = value;
            }
        }

        /// <summary>
        /// "Numeric Value 30-100" : This value sets the skewness of the pie chart (vertical slant).
        /// </summary>
        public int PieYScale
        {
            get
            {
                if (pieYScale == null)
                    throw new NullReferenceException("A propriedade pieYScale não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(pieYScale);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade anchorBgAlpha deve ser um valor inteiro entre 0 e 100.");

                pieYScale = value;
            }
        }

        /// <summary>
        /// "0-100" : This attribute helps you set the border transparency for all the pie borders.
        /// </summary>
        public int PieBorderAlpha
        {
            get
            {
                if (pieBorderAlpha == null)
                    throw new NullReferenceException("A propriedade pieBorderAlpha não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(pieBorderAlpha);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade anchorBgAlpha deve ser um valor inteiro entre 0 e 100.");

                pieBorderAlpha = value;
            }
        }

        /// <summary>
        /// "Numeric Value" : Each pie on the chart has a border, whose thickness you can specify using this attribute.
        /// </summary>
        public int PieBorderThickness
        {
            get
            {
                if (pieBorderThickness == null)
                    throw new NullReferenceException("A propriedade pieBorderThickness não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(pieBorderThickness);
            }
            set
            {
                pieBorderThickness = value;
            }
        }
        

        /// <summary>
        /// "0-100" : This attribute helps you set the transparency for all the pies on the chart.
        /// </summary>
        public int PieFillAlpha
        {
            get
            {
                if (pieFillAlpha == null)
                    throw new NullReferenceException("A propriedade pieFillAlpha não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(pieFillAlpha);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade anchorBgAlpha deve ser um valor inteiro entre 0 e 100.");

                pieFillAlpha = value;
            }
        }
        #endregion
    }
}
