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
    public class BarsNumberFormatPropertySet : PropertySetBase
    {

        #region [ Construtor ]
        public BarsNumberFormatPropertySet()
        {
            divLineDecimalPrecision = null;
            limitsDecimalPrecision = null;
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private int? divLineDecimalPrecision;
        [GraphElement]
        private int? limitsDecimalPrecision;
        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// Number of decimal places to which all divisional line (horizontal) values on the chart would be rounded to.
        /// </summary>
        public int DivLineDecimalPrecision
        {
            get
            {
                if (divLineDecimalPrecision == null)
                    throw new NullReferenceException("A propriedade divLineDecimalPrecision não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(divLineDecimalPrecision);
            }
            set
            {
                divLineDecimalPrecision = value;
            }
        }

        /// <summary>
        ///  Number of decimal places to which upper and lower limit values on the chart would be rounded to.
        /// </summary>
        public int LimitsDecimalPrecision
        {
            get
            {
                if (limitsDecimalPrecision == null)
                    throw new NullReferenceException("A propriedade limitsDecimalPrecision não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(limitsDecimalPrecision);
            }
            set
            {
                limitsDecimalPrecision = value;
            }
        }
        #endregion
    }
}
