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
    public class ChartMarginsPropertySet : PropertySetBase
    {

        #region [ Construtor ]
        public ChartMarginsPropertySet()
        {
            chartLeftMargin = null;
            chartRightMargin = null;
            chartTopMargin = null;
            chartBottomMargin = null;
        }
        #endregion

        #region [ Propriedades Privadas ]
        [GraphElement]
        private int? chartLeftMargin;
        [GraphElement]
        private int? chartRightMargin;
        [GraphElement]
        private int? chartTopMargin;
        [GraphElement]
        private int? chartBottomMargin;
        #endregion

        #region [ Propriedades Publicas ]

        /// <summary>
        /// "Numerical Value (in pixels)" : Space to be left unplotted on the left side of the chart.
        /// </summary>
        public int ChartLeftMargin
        {
            get
            {
                if (chartLeftMargin == null)
                    throw new NullReferenceException("A propriedade chartLeftMargin não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(chartLeftMargin);
            }
            set
            {
                chartLeftMargin = value;
            }
        }

        /// <summary>
        /// "Numerical Value (in pixels)" : Empty space to be left on the right side of the chart
        /// </summary>
        public int ChartRightMargin
        {
            get
            {
                if (chartRightMargin == null)
                    throw new NullReferenceException("A propriedade chartRightMargin não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(chartRightMargin);
            }
            set
            {
                chartRightMargin = value;
            }
        }

        /// <summary>
        /// "Numerical Value (in pixels)" : Empty space to be left on the top of the chart.
        /// </summary>
        public int ChartTopMargin
        {
            get
            {
                if (chartTopMargin == null)
                    throw new NullReferenceException("A propriedade chartTopMargin não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(chartTopMargin);
            }
            set
            {
                chartTopMargin = value;
            }
        }

        /// <summary>
        /// "Numerical Value (in pixels)" : Empty space to be left at the bottom of the chart.
        /// </summary>
        public int ChartBottomMargin
        {
            get
            {
                if (chartBottomMargin == null)
                    throw new NullReferenceException("A propriedade chartBottomMargin não foi setada com nenhum valor válido até o momento.");

                return Convert.ToInt32(chartBottomMargin);
            }
            set
            {
                chartBottomMargin = value;
            }
        }
        #endregion
    }
}
