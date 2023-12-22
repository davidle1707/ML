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
    public class FunnelPropertySet : PropertySetBase
    {
        #region [ Construtor ]
        public FunnelPropertySet()
        {
            fillAlpha = null;
            isSliced = null;
            slicingDistance = null;
            funnelBaseWidth = null;
            funnelBaseHeight = null;
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private int? fillAlpha;
        [GraphElement]
        private bool? isSliced;
        [GraphElement]
        private int? slicingDistance;
        [GraphElement]
        private int? funnelBaseWidth;
        [GraphElement]
        private int? funnelBaseHeight;
        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// 'value(0-100)': This attribute helps you specify the alpha (transparency) of the funnel chart as a whole, i.e., all the funnel segments would be shown in the alpha mentioned in this attribute.
        /// </summary>
        public int FillAlpha
        {
            get
            {
                if (fillAlpha == null)
                    throw new NullReferenceException("A propriedade fillAlpha não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(fillAlpha);
            }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException("A propriedade fillAlpha deve ser um valor inteiro entre 0 e 100.");

                fillAlpha = value;
            }
        }

        /// <summary>
        ///  This attribute specifies whether the the various funnel segments would be sliced, i.e., separated from each other by a distance.
        ///  Default value: true
        /// </summary>
        public bool IsSliced
        {
            get
            {
                if (isSliced == null)
                    throw new NullReferenceException("A propriedade isSliced não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(isSliced);
            }
            set
            {
                isSliced = value;
            }
        }

        /// <summary>
        ///  If you have set the isSliced attribute to 1 or have not defined it at all (so that it takes the value 1 by default), then this attribute specifies the distance (in pixels) by which the various funnel segments would be separated from each other by. Default value: 10
        /// </summary>
        public int SlicingDistance
        {
            get
            {
                if (slicingDistance == null)
                    throw new NullReferenceException("A propriedade slicingDistance não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(slicingDistance);
            }
            set
            {
                slicingDistance = value;
            }
        }

        /// <summary>
        ///  This attribute sets the width of the tap, i.e., the bottom part of the funnel. Default value: If you don't define this attribute, it would be auto-calculated to the most suitable value for the chart.
        /// </summary>
        public int FunnelBaseWidth
        {
            get
            {
                if (funnelBaseWidth == null)
                    throw new NullReferenceException("A propriedade funnelBaseWidth não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(funnelBaseWidth);
            }
            set
            {
                funnelBaseWidth = value;
            }
        }

        /// <summary>
        ///  This attribute sets the height of the tap of the funnel. Default value: If you don't define this attribute, it would be auto-calculated to the best value for the chart.
        /// </summary>
        public int FunnelBaseHeight
        {
            get
            {
                if (funnelBaseHeight == null)
                    throw new NullReferenceException("A propriedade funnelBaseHeight não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(funnelBaseHeight);
            }
            set
            {
                funnelBaseHeight = value;
            }
        }

        #endregion
    }
}
