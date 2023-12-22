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
    public class ChartTitlesPropertySet : PropertySetBase
    {
        #region[ Construtor ]
        public ChartTitlesPropertySet()
        {
            caption = null;
            subCaption = null;
        }
        #endregion

        #region [ Propriedades Privadas ]

        [GraphElement]
        private string caption;
        [GraphElement]
        private string subCaption;

        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// This attribute determines the caption of the chart that would appear at the top of the chart.
        /// </summary>
        public string Caption
        {
            get
            {
                return caption;
            }
            set
            {
                caption = value;
            }
        }

        /// <summary>
        ///  Sub-caption of the chart
        /// </summary>
        public string SubCaption
        {
            get
            {
                return subCaption;
            }
            set
            {
                subCaption = value;
            }
        }

        #endregion
    }
}
