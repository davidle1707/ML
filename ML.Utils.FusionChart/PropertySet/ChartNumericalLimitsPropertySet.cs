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
    public class ChartNumericalLimitsPropertySet : PropertySetBase
    {
        #region [ Construtor ]
        public ChartNumericalLimitsPropertySet()
        {
            yAxisMinValue = null;
            xAxisMinValue = null;
        }
        #endregion

        #region [ Propriedades Privadas ]

        [GraphElement]
        private decimal? yAxisMinValue;
        [GraphElement]
        private decimal? xAxisMinValue;

        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// This attribute determines the lower limit of y-axis.
        /// </summary>
        public decimal YAxisMinValue
        {
            get
            {
                if (yAxisMinValue == null)
                    throw new NullReferenceException("A propriedade yAxisMinValue não foi setada com nenhum valor válido até o momento.");

                return Convert.ToDecimal(yAxisMinValue);
            }
            set
            {
                yAxisMinValue = value;
            }
        }

        /// <summary>
        /// This attribute determines the upper limit of y-axis.
        /// </summary>
        public decimal XAxisMinValue
        {
            get
            {
                if (xAxisMinValue == null)
                    throw new NullReferenceException("A propriedade xAxisMinValue não foi setada com nenhum valor válido até o momento.");

                return Convert.ToDecimal(xAxisMinValue);
            }
            set
            {
                xAxisMinValue = value;
            }
        }

        #endregion
    }
}
