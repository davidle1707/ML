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
    public abstract class MultiSeriesChart : FusionChartBase
    {
        #region [ Private Properties ]
        private MSCategory categorySet;
        private List<MSDataSet> dataSetList;
        private object dataSource;

        private Boolean dataSourceBound = false;
        #endregion

        #region [ Public Properties ]

        /// <summary>
        /// Each category element represents a x-axis data label.
        /// </summary>
        public MSCategory CategorySet
        {
            get
            {
                if (categorySet == null)
                    categorySet = new MSCategory();
                return categorySet;
            }
            set
            {
                categorySet = value;
            }
        }

        /// <summary>
        /// DataSet is a list of Set Elements that determines a set of data which would appear on the graph.
        /// </summary>
        public List<MSDataSet> DataSetList
        {
            get
            {
                if (dataSetList == null)
                    dataSetList = new List<MSDataSet>();
                return dataSetList;
            }
            set { dataSetList = value; }
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
        /// Fields Name for the category.
        /// </summary>
        public string DataCategoryTextField { set; private get; }

        /// <summary>
        /// Fields Name for the series.
        /// </summary>
        public string DataSeriesTextField { set; private get; }

        /// <summary>
        /// Fields Name for the series value.
        /// </summary>
        public string DataSeriesValueField { set; private get; }

        /// <summary>
        /// Indicate that the data set in DataSource property is already sorted.
        /// Useful when you have a data already sorted in the way that you want to show.
        /// If set to false(default) LiberoAPI will sort the data before parse it.
        /// Default: false;
        /// </summary>
        public bool IsDataSourceSorted { get; set; }

        #endregion

        #region [ Public Methods ]

        /// <summary>
        /// Apply (true or false) for all Show Names. 
        /// </summary>
        /// <param name="value">True(Show) ou False(Hide)</param>
        public void SetShowName(bool value)
        {
            foreach (MSCategoryElement element in this.CategorySet.CategoryElementSet)
                element.ShowName = value;
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

            if (this is ICombinationChart)
                this.ApplyCombinationBehavior();


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
            if (this.dataSetList == null)
                throw new InvalidOperationException("The method SetTemplate must be called AFTER your data be set. To set the data use the DataSetList and CategorySet properties or DataSource property and DataBind method.");

            base.Template.Reset();

            for (int i = 0; i < this.dataSetList.Count; i++)
            {
                this.dataSetList[i].Color = base.Template.GetSeriesColor();
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
            if (String.IsNullOrEmpty(this.DataCategoryTextField)
                    || String.IsNullOrEmpty(this.DataSeriesTextField)
                    || String.IsNullOrEmpty(this.DataSeriesValueField)
                    || this.dataSource == null)
                throw new ArgumentException("You must set DataCategoryTextField, DataSeriesTextField, DataSeriesValueField and DataSource BEFORE calling DataBind.");

            MSDataSourceParser parser = new MSDataSourceParser();
            parser.DataCategoryTextField = this.DataCategoryTextField;
            parser.DataSeriesTextField = this.DataSeriesTextField;
            parser.DataSeriesValueField = this.DataSeriesValueField;
            parser.IsDataSourceSorted = this.IsDataSourceSorted;

            parser.Parse(this.dataSource);
            this.CategorySet = parser.GetCategorySet();
            this.DataSetList = parser.GetDataSetList();

            this.dataSourceBound = true;
        }

        #endregion

        #region [ Private Methods ]

        private void ApplyCombinationBehavior()
        {
            if (this.dataSetList == null)
                return;
            if (!(this is ICombinationChart))
                throw new InvalidOperationException("You can only call ApplyCombinationBehavior on ICombinationChart members.");

            Dictionary<String, CombinationAxisType> axisDic = ((ICombinationChart)this).GetAxisTypeDictionary();

            for (int i = 0; i < this.DataSetList.Count; i++)
            {
                MSDataSet cds = this.DataSetList[i];
                if (axisDic.Keys.Contains(cds.SeriesName))
                    cds.ParentYAxis = axisDic[cds.SeriesName] == CombinationAxisType.Primary ? "P" : "S";
            }
        }

        #endregion
    }
}
