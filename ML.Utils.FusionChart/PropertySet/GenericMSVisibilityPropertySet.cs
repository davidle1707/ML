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
using ML.Utils.FusionChart.Attribute;

namespace ML.Utils.FusionChart.PropertySet
{
    public class GenericMSVisibilityPropertySet : GenericVisibilityPropertySet
    {
        #region[ Construtor ]
        public GenericMSVisibilityPropertySet()
            : base()
        {
            showLegend = null;
        }
        #endregion

        #region [ Propriedades Privadas ]

        [GraphElement]
        private bool? showLegend;

        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// This attribute sets whether the legend would be displayed at the bottom of the chart.
        /// </summary>
        public bool ShowLegend
        {
            get
            {
                if (showLegend == null)
                    throw new NullReferenceException("A propriedade showLegend não foi setada com nenhum valor válido até o momento.");

                return Convert.ToBoolean(showLegend);
            }
            set
            {
                showLegend = value;
            }
        }

        #endregion
    }
}
