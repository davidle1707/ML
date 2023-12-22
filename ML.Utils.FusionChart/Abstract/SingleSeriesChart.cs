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
using System.Reflection;
using ML.Utils.FusionChart.Attribute;
using ML.Utils.FusionChart.PropertySet;
using ML.Utils.FusionChart.Template;

namespace ML.Utils.FusionChart.Abstract
{
    public abstract class SingleSeriesChart : FusionChartBase
    {
        #region [ Private Properties ]
        private List<SSSetElement> set = null;
        private object dataSource = null;

        private Boolean dataSourceBound = false;
        #endregion

        #region [ Public Properties ]

        /// <summary>
        /// Set of chart data.
        /// </summary>
        public List<SSSetElement> Set
        {
            get
            {
                if (set == null)
                    set = new List<SSSetElement>();
                return set;
            }
            set { set = value; }
        }

        /// <summary>
        /// Data Source. 
        /// Can be ONLY a DataTable or a member that implements IList.
        /// </summary>
        public object DataSource
        {
            set
            {
                this.dataSource = value;
                this.dataSourceBound = false;
            }
            private get { return this.dataSource; }
        }

        /// <summary>
        /// Data text fields name
        /// </summary>
        public string DataTextField { set; private get; }

        /// <summary>
        /// Data value fields name
        /// </summary>
        public string DataValueField { set; private get; }

        #endregion

        #region [ Public Methods ]

        /// <summary>
        /// Apply (true or false) for all Show Names. 
        /// </summary>
        /// <param name="value">True(Show) or False(Hide)</param>
        public void SetShowName(bool value)
        {
            foreach (SSSetElement set in this.Set)
                set.ShowName = value;
        }

        /// <summary>
        /// Get the XML supported by Fusion Charts.
        /// </summary>
        /// <returns>String XML</returns>
        public override string ToXML()
        {
            if (this.dataSource != null && this.dataSourceBound == false)
                this.DataBind();
            if (base.Template != null && base.templateApplied == false)
                this.ApplyTemplate();

            FusionChartXMLSerializer fusionChatsXMLSerializer = new FusionChartXMLSerializer();
            return fusionChatsXMLSerializer.Serialize(this);
        }

        /// <summary>
        /// Apply a template in the chart.
        /// Must be applied AFTER to call DataBind if you had set DataSource property.
        /// </summary>
        public override void ApplyTemplate()
        {
            if (base.Template == null)
                throw new InvalidOperationException("You must set Template property before to apply a template");
            if (this.set == null)
                throw new InvalidOperationException("The method SetTemplate must be called AFTER your data be set. To set the data use the Set property or DataSource property and DataBind method.");

            base.Template.Reset();

            for (int i = 0; i < set.Count; i++)
            {
                set[i].Color = base.Template.GetSeriesColor();
            }


            foreach (PropertyInfo propertyInfo in base.Template.GetType().GetProperties())//Todas as prop do Template
            {
                if (propertyInfo.PropertyType.BaseType.Equals(typeof(PropertySetBase))) // Verifica se herda de 'PropertySetBase' (prop de configuração de cores por exemplo)
                {
                    if (propertyInfo.GetValue(base.Template, null) != null) // verifica se o template tem valor para essa propriedade
                    {
                        foreach (PropertyInfo thisPropertyInfo in this.GetType().GetProperties()) //busca propriedades do objeto de grafico
                        {
                            if (thisPropertyInfo.PropertyType.Equals(propertyInfo.PropertyType)) //verifica se é a mesma propriedade
                            {
                                thisPropertyInfo.SetValue(this, propertyInfo.GetValue(base.Template, null), null); //Aplica a propriedade do Template no Objeto do gráfico.
                            }
                        }
                    }
                }
            }
            base.templateApplied = true;
        }

        /// <summary>
        /// Load data from the DataSource property to the chart properties.  
        /// </summary>
        public void DataBind()
        {
            if (String.IsNullOrEmpty(this.DataTextField)
                || String.IsNullOrEmpty(this.DataValueField)
                || this.dataSource == null)
                throw new ArgumentException("You must FIRST set DataTextField, DataValueField and DataSource before calling the DataBind.");

            SSDataSourceParser parser = new SSDataSourceParser();
            parser.DataTextField = this.DataTextField;
            parser.DataValueField = this.DataValueField;

            this.Set = parser.Parse(this.dataSource);

            this.dataSourceBound = true;
        }

        #endregion
    }
}
