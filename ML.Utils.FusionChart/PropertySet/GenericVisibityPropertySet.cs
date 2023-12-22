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
    public class GenericVisibilityPropertySet : PropertySetBase
    {
        #region[ Construtor ]
        public GenericVisibilityPropertySet()
        {
            showNames = null;
            showValues = null;
        }
        #endregion

        #region [ Propriedades Privadas ]

        [GraphElement]
        private bool? showNames;
        [GraphElement]
        private bool? showValues;

        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// This attribute can have either of the two possible values: 1,0. It sets the configuration whether the x-axis values (for the data sets) will be displayed or not. By default, this attribute assumes the value true, which means that the x-axis names will be displayed.
        /// </summary>
        public bool ShowNames
        {
            get
            {
                if (showNames == null)
                    throw new NullReferenceException("A propriedade showNames não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showNames);
            }
            set
            {
                showNames = value;
            }
        }

        /// <summary>
        /// This attribute can have either of the two possible values: 1,0. It sets the configuration whether the data numerical values will be displayed along with the columns, bars, lines and the pies. By default, this attribute assumes the value true, which means that the values will be displayed.
        /// </summary>
        public bool ShowValues
        {
            get
            {
                if (showValues == null)
                    throw new NullReferenceException("A propriedade showValues não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showValues);
            }
            set
            {
                showValues = value;
            }
        }

        #endregion
    }
}
