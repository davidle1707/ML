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
using ML.Utils.FusionChart.PropertySet;
using ML.Utils.FusionChart.Template;

namespace ML.Utils.FusionChart.Abstract
{
    public abstract class FusionChartBase
    {
        #region [ Construtor ]
        public FusionChartBase()
        {
            Background = new BackgroundPropertySet();
            ChartTitles = new ChartTitlesPropertySet();
            Animation = new AnimationPropertySet();
        }
        #endregion

        #region [ Private Properties ]
        private List<TrendLineElement> trendLines;
        private FusionChartsTemplateBase template;
        #endregion

        #region [ Protected Properties ]
        protected Boolean templateApplied = false;
        #endregion

        #region [ Public Properties ]

        /// <summary>
        /// Background Set of Properties.
        /// </summary>
        public BackgroundPropertySet Background { get; set; }

        /// <summary>
        /// Title Set of Properties
        /// </summary>
        public ChartTitlesPropertySet ChartTitles { get; set; }

        /// <summary>
        /// Animation property
        /// </summary>
        public AnimationPropertySet Animation { get; set; }

        /// <summary>
        /// Using the trendLines element (and child elements), you can define trend lines on the charts. Trend lines are the horizontal lines spanning the chart canvas that aid in interpretation of data with respect to some previous pre-determined figure.
        /// </summary>
        public List<TrendLineElement> TrendLines
        {
            get
            {
                if (trendLines == null)
                    trendLines = new List<TrendLineElement>();
                return trendLines;
            }
            set { trendLines = value; }
        }

        /// <summary>
        /// Define a Template for a chart. You should use a object that inherits from FusionChartsTemplate.
        /// </summary>
        public FusionChartsTemplateBase Template
        {
            get { return template; }
            set
            {
                this.template = value;
                this.templateApplied = false;
            }
        }

        #endregion

        #region [ Public Methods ]

        /// <summary>
        /// Get the XML supported by Fusion Charts.
        /// </summary>
        /// <returns>String XML</returns>
        public abstract string ToXML();

        /// <summary>
        /// Return a string of the Chart Type.
        /// </summary>
        public string ChartType
        {
            get { return this.GetType().Name; }
            private set { }
        }

        /// <summary>
        /// Apply a template in the chart.
        /// Must be applied AFTER to call DataBind if you had set DataSource property.
        /// </summary>
        public abstract void ApplyTemplate();

        #endregion
    }
}
