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
    public class AxisTitlesPropertySet : PropertySetBase
    {
        #region [ Construtor ]
        public AxisTitlesPropertySet()
        {
            xAxisName = null;
            yAxisName = null;
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private string xAxisName;
        [GraphElement]
        private string yAxisName;
        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// "String" : x-Axis text title (if the chart supports axis)
        /// </summary>
        public string XAxisName
        {
            get{return xAxisName;}
            set{xAxisName = value;}
        }

        /// <summary>
        /// "String" : y-Axis text title (if the chart supports axis)
        /// </summary>
        public string YAxisName
        {
            get
            {
                return yAxisName;
            }
            set
            {
                yAxisName = value;
            }
        }

        #endregion
    }
}
