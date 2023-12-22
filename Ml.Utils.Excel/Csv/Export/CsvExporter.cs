using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Xml;

namespace ML.Utils.Excel.Csv.Export
{
    /// <summary>
    /// Exporter that uses Expression tree parsing to work out what values to export for 
    /// columns, and will use additional data as specified in the List of ExportableColumn
    /// which defines whethere to use custom headers, or formatted output
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CsvExporter<T> where T : class
    {
        private List<ExportableColumn<T>> columns = new List<ExportableColumn<T>>();
        private Dictionary<Expression<Func<T, Object>>, Func<T, Object>> compiledFuncLookup =
            new Dictionary<Expression<Func<T, Object>>, Func<T, Object>>();
        private List<String> headers = new List<String>();
        private IEnumerable<T> sourceList;
        private String seperator;
        private bool doneHeaders;

        public CsvExporter(IEnumerable<T> sourceList, String seperator = ",")
        {
            this.sourceList = sourceList;
            this.seperator = seperator;
        }

        public CsvExporter<T> AddExportableColumn(Expression<Func<T, Object>> func, string headerString = "", string customFormatString = "")
        {
            columns.Add(new ExportableColumn<T>(func, headerString, customFormatString));
            return this;
        }

        /// <summary>
        /// cols: key (header) - value (func value)
        /// </summary>
        public CsvExporter<T> AddExportableColumns(Dictionary<string, Expression<Func<T, Object>>> cols, string customFormatString = "")
        {
            foreach (var col in cols)
            {
                columns.Add(new ExportableColumn<T>(col.Value, col.Key, customFormatString));
            }

            return this;
        }

        /// <summary>
        /// Export all specified columns as a string, 
        /// using seperator and column data provided
        /// where we may use custom or default headers 
        /// (depending on whether a custom header string was supplied)
        /// where we may use custom fomatted column data or default data 
        /// (depending on whether a custom format string was supplied)
        /// </summary>
        public void ToCsv(TextWriter writer)
        {
            if (columns.Count == 0)
                throw new InvalidOperationException(
                    "You need to specify at least one column to export value");

            foreach (T item in sourceList)
            {
                List<String> values = new List<String>();
                foreach (ExportableColumn<T> exportableColumn in columns)
                {
                    if (!doneHeaders)
                    {
                        if (String.IsNullOrEmpty(exportableColumn.HeaderString))
                        {
                            headers.Add(GetPropertyName(exportableColumn.Func));
                        }
                        else
                        {
                            headers.Add(exportableColumn.HeaderString);
                        }

                        Func<T, Object> func = exportableColumn.Func.Compile();
                        compiledFuncLookup.Add(exportableColumn.Func, func);
                        if (!String.IsNullOrEmpty(exportableColumn.CustomFormatString))
                        {
                            var value = func(item);
                            values.Add(value != null ?
                                String.Format(exportableColumn.CustomFormatString, EscapeCSVSpecialChar(value.ToString())) : "");

                        }
                        else
                        {
                            var value = func(item);
                            values.Add(value != null ? EscapeCSVSpecialChar(value.ToString()) : "");
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(exportableColumn.CustomFormatString))
                        {
                            var value = compiledFuncLookup[exportableColumn.Func](item);
                            values.Add(value != null ?
                                String.Format(exportableColumn.CustomFormatString, EscapeCSVSpecialChar(value.ToString())) : "");
                        }
                        else
                        {
                            var value = compiledFuncLookup[exportableColumn.Func](item);
                            values.Add(value != null ? EscapeCSVSpecialChar(value.ToString()) : "");
                        }
                    }
                }
                if (!doneHeaders)
                {
                    writer.WriteLine(string.Join(seperator, headers.Select(SafeStringCsv)));
                    doneHeaders = true;
                }
                writer.WriteLine(string.Join(seperator, values.Select(SafeStringCsv)));
            }

        }

        // <summary>
        /// Export all specified columns as a XML string, using column data provided
        /// and use custom headers depending on whether a custom header string was supplied.
        /// Use custom formatted column data or default data depending on whether a custom format string was supplied.
        /// </summary>
        public void ToXml(XmlTextWriter writer)
        {
            if (columns.Count == 0)
                throw new InvalidOperationException("You need to specify at least one element to export value");

            foreach (T item in sourceList)
            {
                List<String> values = new List<String>();
                foreach (ExportableColumn<T> exportableColumn in columns)
                {
                    if (!doneHeaders)
                    {
                        if (String.IsNullOrEmpty(exportableColumn.HeaderString))
                        {
                            headers.Add(MakeXMLNameLegal(GetPropertyName(exportableColumn.Func)));
                        }
                        else
                        {
                            headers.Add(MakeXMLNameLegal(exportableColumn.HeaderString));
                        }

                        Func<T, Object> func = exportableColumn.Func.Compile();
                        compiledFuncLookup.Add(exportableColumn.Func, func);
                        if (!String.IsNullOrEmpty(exportableColumn.CustomFormatString))
                        {
                            var value = func(item);
                            values.Add(value != null ?
                                String.Format(exportableColumn.CustomFormatString, value.ToString()) : "");

                        }
                        else
                        {
                            var value = func(item);
                            values.Add(value != null ? value.ToString() : "");
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(exportableColumn.CustomFormatString))
                        {
                            var value = compiledFuncLookup[exportableColumn.Func](item);
                            values.Add(value != null ?
                                String.Format(exportableColumn.CustomFormatString, value.ToString()) : "");
                        }
                        else
                        {
                            var value = compiledFuncLookup[exportableColumn.Func](item);
                            values.Add(value != null ? value.ToString() : "");
                        }
                    }
                }
                if (!doneHeaders)
                {
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartDocument(true);
                    writer.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='dump.xsl'");
                    writer.WriteComment("List Exporter dump");

                    // Write main document node and document properties
                    writer.WriteStartElement("dump");
                    writer.WriteAttributeString("date", DateTime.Now.ToString());

                    doneHeaders = true;
                }

                writer.WriteStartElement("item");
                for (int i = 0; i < values.Count; i++)
                {
                    writer.WriteStartElement(headers[i]);
                    writer.WriteString(values[i]);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Flush();
        }

        /// <summary>
        /// Export to file, using the AsCSVString() method to supply the exportable data
        /// </summary>
        public void WhichIsExportedToFileLocation(StreamWriter fileWriter)
        {
            ToCsv(fileWriter);
        }

        /// <summary>
        /// Gets a Name from an expression tree that is assumed to be a
        /// MemberExpression
        /// </summary>
        private static string GetPropertyName<TEntity>(
            Expression<Func<TEntity, Object>> propertyExpression)
        {
            var lambda = propertyExpression as LambdaExpression;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;

            return propertyInfo.Name;
        }

        private string MakeXMLNameLegal(string aString)
        {
            StringBuilder newName = new StringBuilder();

            if (!char.IsLetter(aString[0]))
                newName.Append("_");

            // Must start with a letter or underscore.
            for (int i = 0; i <= aString.Length - 1; i++)
            {
                if (char.IsLetter(aString[i]) || char.IsNumber(aString[i]))
                {
                    newName.Append(aString[i]);
                }
                else
                {
                    newName.Append("_");
                }
            }
            return newName.ToString();
        }

        private string SafeStringCsv(string source)
        {
            return source.Replace(this.seperator, " ");
        }

        // CSV rules: http://en.wikipedia.org/wiki/Comma-separated_values#Basic_rules
        // From the rules:
        // 1. if the data has quote, escape the quote in the data
        // 2. if the data contains the delimiter (in our case ','), double-quote it
        // 3. if the data contains the new-line, double-quote it.
        private string EscapeCSVSpecialChar(string data)
        {
            if (data.Contains("\""))
            {
                data = data.Replace("\"", "\"\"");
            }

            if (data.Contains(","))
            {
                data = String.Format("\"{0}\"", data);
            }

            if (data.Contains(Environment.NewLine))
            {
                data = String.Format("\"{0}\"", data);
            }

            return data;
        }
    }
}
