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
using System.Data;
using System.Collections;

namespace ML.Utils.FusionChart
{
    internal class MSDataSourceParser
    {
        internal MSDataSourceParser()
        {
            categorySet = new MSCategory();
            dataSetList = new List<MSDataSet>();
        }

        private MSCategory categorySet;
        private List<MSDataSet> dataSetList;

        internal string DataSeriesValueField { set; private get; }
        internal string DataCategoryTextField { set; private get; }
        internal string DataSeriesTextField { set; private get; }
        internal bool IsDataSourceSorted { set; private get; }

        internal void Parse(object dataSource)
        {
            if (dataSource is DataTable)
            {
                Parse((DataTable)dataSource);
                return;
            }
            if (dataSource is IList)
            {
                Parse((IList)dataSource);
                return;
            }

            throw new ArgumentException("The parameter must be a DataTable or a member thar implements IList.");
        }

        private void Parse(DataTable dtDataTable)
        {
            #region [ categorySet ]
            string[] sCategories = dtDataTable.AsEnumerable().Select(c => c.Field<String>(this.DataCategoryTextField)).Distinct().ToArray();

            if (!this.IsDataSourceSorted)
                sCategories = (from c in sCategories orderby c select c).ToArray();

            foreach (string sCategory in sCategories)
                categorySet.CategoryElementSet.Add(new MSCategoryElement(sCategory));
            #endregion

            #region [ sort series,category ]
            if (!this.IsDataSourceSorted)
                dtDataTable.DefaultView.Sort = this.DataSeriesTextField + "," + this.DataCategoryTextField;
            #endregion

            #region [ dataSetList ]
            String sPrevSeries = "$#$#$#StringQUASEQueImpossivelDeSerUmaSerieValida)(*!@&";
            foreach (DataRowView dr in dtDataTable.DefaultView)
            {
                String sCategory = dr[this.DataCategoryTextField].ToString();
                String sSeries = dr[this.DataSeriesTextField].ToString();
                decimal nSeriesValue = dr[this.DataSeriesValueField] == null ? 0 :
                    Convert.ToDecimal(dr[this.DataSeriesValueField]);

                if (sPrevSeries != sSeries)
                {
                    sPrevSeries = sSeries;
                    dataSetList.Add(new MSDataSet(sSeries));
                }

                dataSetList.Last().SetElementSet.Add(new MSSetElement(nSeriesValue));

            }
            #endregion
        }

        private void Parse(IList iList)
        {
            #region [ prepara data graph ]
            List<DataGraph> sList = new List<DataGraph>();
            for (int i = 0; i < iList.Count; i++)
            {
                Type type = iList[i].GetType();
                string sCategory = type.GetProperty(this.DataCategoryTextField).GetValue(iList[i], null).ToString();
                string sSeries = type.GetProperty(this.DataSeriesTextField).GetValue(iList[i], null).ToString();
                decimal nSeriesValue = type.GetProperty(this.DataSeriesValueField).GetValue(iList[i], null) == null ? 0 :
                    Convert.ToDecimal(type.GetProperty(this.DataSeriesValueField).GetValue(iList[i], null));

                sList.Add(
                    new DataGraph
                    {
                        CategoryText = sCategory,
                        SeriesText = sSeries,
                        SeriesValue = nSeriesValue
                    }
                );

            }
            iList = null;
            #endregion

            #region [ categorySet ]
            string[] sCategories = sList.Select(c => c.CategoryText).Distinct().ToArray();

            if (!this.IsDataSourceSorted)
                sCategories = (from c in sCategories orderby c select c).ToArray();

            foreach (string sCategory in sCategories)
                categorySet.CategoryElementSet.Add(new MSCategoryElement(sCategory));
            #endregion

            #region [ sort ]
            if (!this.IsDataSourceSorted)
                sList = (from c in sList orderby c.SeriesText, c.CategoryText select c).ToList();
            #endregion

            #region [ dataSetList ]
            String sPrevSeries = "$#$#$#StringQUASEImpossivelDeSerUmaSerieValida)(*!@&";
            for (int i = 0; i < sList.Count; i++)
            {
                if (sPrevSeries != sList[i].SeriesText)
                {
                    sPrevSeries = sList[i].SeriesText;
                    dataSetList.Add(new MSDataSet(sList[i].SeriesText));
                }

                dataSetList.Last().SetElementSet.Add(new MSSetElement(sList[i].SeriesValue));

            }
            #endregion

        }

        internal MSCategory GetCategorySet()
        {
            return categorySet;
        }

        internal List<MSDataSet> GetDataSetList()
        {
            return dataSetList;
        }

        private class DataGraph
        {
            public string CategoryText;
            public string SeriesText;
            public decimal SeriesValue;
        }

    }

}
