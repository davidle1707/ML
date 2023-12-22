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
using System.Xml;
using System.IO;
using System.Reflection;
using System.Globalization;
using ML.Utils.FusionChart.Abstract;
using ML.Utils.FusionChart.Attribute;

namespace ML.Utils.FusionChart
{
    internal class FusionChartXMLSerializer
    {
        #region [ Internal Methods ]

        /// <summary>
        /// Serialize o object of SingleSeriesChart in a XML supported by fusion Charts.
        /// </summary>
        /// <param name="chart">Chart - Type SingleSeriesChart</param>
        /// <returns></returns>
        internal String Serialize(SingleSeriesChart chart)
        {
            if (chart == null)
                throw new ArgumentNullException();

            String sAtt;
            if (HasAttribute(chart.GetType(), typeof(GraphAttribute)))
                sAtt = "graph";
            else if(HasAttribute(chart.GetType(), typeof(ChartAttribute)))
                sAtt = "chart";
            else
                throw new ArgumentException("Chart must have a Graph or Chart attribute");

            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement(sAtt);

            #region [ Fields of PropertySet ]
            Type type = chart.GetType();
            foreach (PropertyInfo propertyInfoSup in type.GetProperties())
            {
                foreach (FieldInfo fieldInfo in propertyInfoSup.PropertyType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    if (HasAttribute(fieldInfo, typeof(GraphElementAttribute)))
                    {
                        object val = fieldInfo.GetValue(
                            propertyInfoSup.GetValue(chart, null)
                            );

                        if (val != null)
                        {
                            xmlTextWriter.WriteAttributeString(fieldInfo.Name
                                , ParseToXMLString(val));
                        }
                    }
                }
            }
            #endregion

            #region [ Properties of Set ]
            if (chart.Set != null && chart.Set.Count > 0)
            {
                foreach (SSSetElement set in chart.Set)
                {
                    xmlTextWriter.WriteStartElement("set");

                    Type typeSet = set.GetType();
                    foreach (PropertyInfo propertyInfo in typeSet.GetProperties())
                    {
                        if (HasAttribute(propertyInfo, typeof(SetElementSSAttribute)))
                        {
                            object val = propertyInfo.GetValue(set, null);

                            if (val != null)
                            {
                                xmlTextWriter.WriteAttributeString(propertyInfo.Name
                                    , ParseToXMLString(val));
                            }
                        }
                    }

                    xmlTextWriter.WriteEndElement();
                }
            }
            #endregion

            #region [ Trend Lines ]
            if (chart.TrendLines != null && chart.TrendLines.Count > 0)
            {
                xmlTextWriter.WriteStartElement("trendlines");
                foreach (TrendLineElement trendLine in chart.TrendLines)
                {
                    xmlTextWriter.WriteStartElement("line");

                    Type typeLine = trendLine.GetType();
                    foreach (PropertyInfo propertyInfo in typeLine.GetProperties())
                    {
                        if (HasAttribute(propertyInfo, typeof(TrendLineElementAttribute)))
                        {
                            object val = propertyInfo.GetValue(trendLine, null);

                            if (val != null)
                            {
                                xmlTextWriter.WriteAttributeString(propertyInfo.Name
                                    , ParseToXMLString(val));
                            }
                        }
                    }
                    xmlTextWriter.WriteEndElement();
                }
                xmlTextWriter.WriteEndElement();
            }
            #endregion

            xmlTextWriter.WriteEndElement();
            return stringWriter.GetStringBuilder().ToString();
        }


        /// <summary>
        /// Serialize o object of SingleSeriesChart in a XML supported by fusion Charts.
        /// </summary>
        /// <param name="chart">Chart - Type MultiSeriesChart</param>
        /// <returns></returns>
        internal String Serialize(MultiSeriesChart chart)
        {
            if (chart == null)
                throw new ArgumentNullException();

            String sAtt;
            if (HasAttribute(chart.GetType(), typeof(GraphAttribute)))
                sAtt = "graph";
            else if (HasAttribute(chart.GetType(), typeof(ChartAttribute)))
                sAtt = "chart";
            else
                throw new ArgumentException("Chart must have a Graph or Chart attribute");


            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement(sAtt);

            #region [ Fields of PropertySet ]
            Type type = chart.GetType();
            foreach (PropertyInfo propertyInfoSup in type.GetProperties())
            {
                foreach (FieldInfo fieldInfo in propertyInfoSup.PropertyType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    if (HasAttribute(fieldInfo, typeof(GraphElementAttribute)))
                    {
                        object val = fieldInfo.GetValue(
                            propertyInfoSup.GetValue(chart, null)
                            );

                        if (val != null)
                        {
                            xmlTextWriter.WriteAttributeString(fieldInfo.Name
                                , ParseToXMLString(val));
                        }
                    }
                }
            }
            #endregion

            #region [ Category ]
            if (chart.CategorySet != null && chart.CategorySet.CategoryElementSet.Count > 0)
            {
                xmlTextWriter.WriteStartElement("categories");
                Type typeSet = chart.CategorySet.GetType();
                foreach (PropertyInfo propertyInfo in typeSet.GetProperties())
                {
                    if (HasAttribute(propertyInfo, typeof(CategoryMSAttribute)))
                    {
                        object val = propertyInfo.GetValue(chart.CategorySet, null);

                        if (val != null)
                        {
                            xmlTextWriter.WriteAttributeString(propertyInfo.Name
                                , ParseToXMLString(val));
                        }
                    }
                }

                foreach (MSCategoryElement categoryElement in chart.CategorySet.CategoryElementSet)
                {
                    xmlTextWriter.WriteStartElement("category");

                    typeSet = categoryElement.GetType();
                    foreach (PropertyInfo propertyInfo in typeSet.GetProperties())
                    {
                        if (HasAttribute(propertyInfo, typeof(CategoryElementMSAttribute)))
                        {
                            object val = propertyInfo.GetValue(categoryElement, null);

                            if (val != null)
                            {
                                xmlTextWriter.WriteAttributeString(propertyInfo.Name
                                    , ParseToXMLString(val));
                            }
                        }
                    }

                    xmlTextWriter.WriteEndElement();
                }

                xmlTextWriter.WriteEndElement();
            }
            #endregion

            #region [ DataSet ]
            if (chart.DataSetList != null && chart.DataSetList.Count > 0)
            {
                foreach (MSDataSet dataSet in chart.DataSetList)
                {
                    if (dataSet.SetElementSet != null && dataSet.SetElementSet.Count > 0)
                    {
                        xmlTextWriter.WriteStartElement("dataset");
                        Type typeSet = dataSet.GetType();
                        foreach (PropertyInfo propertyInfo in typeSet.GetProperties())
                        {
                            if (HasAttribute(propertyInfo, typeof(DatasetMSAttribute)))
                            {
                                object val = propertyInfo.GetValue(dataSet, null);

                                if (val != null)
                                {
                                    xmlTextWriter.WriteAttributeString(propertyInfo.Name
                                        , ParseToXMLString(val));
                                }
                            }
                        }

                        foreach (MSSetElement setElement in dataSet.SetElementSet)
                        {
                            xmlTextWriter.WriteStartElement("set");

                            typeSet = setElement.GetType();
                            foreach (PropertyInfo propertyInfo in typeSet.GetProperties())
                            {
                                if (HasAttribute(propertyInfo, typeof(SetElementMSAttribute)))
                                {
                                    object val = propertyInfo.GetValue(setElement, null);

                                    if (val != null)
                                    {
                                        xmlTextWriter.WriteAttributeString(propertyInfo.Name
                                            , ParseToXMLString(val));
                                    }
                                }
                            }

                            xmlTextWriter.WriteEndElement();
                        }

                        xmlTextWriter.WriteEndElement();
                    }
                }
            }
            #endregion

            #region [ Trend Lines ]
            if (chart.TrendLines != null && chart.TrendLines.Count > 0)
            {
                xmlTextWriter.WriteStartElement("trendlines");
                foreach (TrendLineElement trendLine in chart.TrendLines)
                {
                    xmlTextWriter.WriteStartElement("line");

                    Type typeLine = trendLine.GetType();
                    foreach (PropertyInfo propertyInfo in typeLine.GetProperties())
                    {
                        if (HasAttribute(propertyInfo, typeof(TrendLineElementAttribute)))
                        {
                            object val = propertyInfo.GetValue(trendLine, null);

                            if (val != null)
                            {
                                xmlTextWriter.WriteAttributeString(propertyInfo.Name
                                    , ParseToXMLString(val));
                            }
                        }
                    }
                    xmlTextWriter.WriteEndElement();
                }
                xmlTextWriter.WriteEndElement();
            }
            #endregion

            xmlTextWriter.WriteEndElement();
            return stringWriter.GetStringBuilder().ToString();
        }

        #endregion

        #region [ Private Methods ]

        /// <summary>
        /// Verify an attribute.
        /// </summary>
        /// <param name="info">MemberInfo</param>
        /// <param name="type">Type to compare</param>
        /// <returns></returns>
        private bool HasAttribute(MemberInfo info, Type type)
        {
            object[] attributes = info.GetCustomAttributes(true);
            for (int i = 0; i < attributes.Length; i++)
            {
                if (attributes[i].GetType().Equals(type))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private string ParseToXMLString(object val)
        {
            if (val is Nullable<bool>)
                val = (int)(((bool)val) ? 1 : 0);
            if (val is bool)
                val = (int)(((bool)val) ? 1 : 0);
            if (val is decimal)
                val = ((decimal)val).ToString(CultureInfo.InvariantCulture);

            return val.ToString();
        }

        #endregion
    }
}
