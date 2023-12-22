using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ML.Utils.Excel.Csv;
using ML.Utils.Excel.Csv.Export;

namespace ML.Utils.Excel
{
    public static class CsvHelper
    {
        public static ExcelSheetInfo GetSheet(string filePath)
        {
            var dt = ToDataTable(filePath, onlyColumns: true);
            return new ExcelSheetInfo
            {
                Name = "CSV",
                Columns = dt.Columns?.Count > 0 ? dt.Columns.Cast<DataColumn>().Select(a => a.ColumnName).ToList() : new List<string>()
            };
        }

        public static DataTable ToDataTable(string filePath, int maxRecords = -1, bool addIndexCol = true, string indexColName = "Index", bool onlyColumns = false)
        {
            using (var stream = new StreamReader(filePath))
            {
                return ToDataTable(stream, maxRecords, addIndexCol, indexColName, onlyColumns);
            }
        }

        public static DataTable ToDataTable(Stream stream, int maxRecords = -1, bool addIndexCol = true, string indexColName = "Index", bool onlyColumns = false)
        {
            using (var textReader = new StreamReader(stream))
            {
                return ToDataTable(textReader, maxRecords, addIndexCol, indexColName, onlyColumns);
            }
        }

        public static DataTable ToDataTable(TextReader textReader, int maxRecords = -1, bool addIndexCol = true, string indexColName = "Index", bool onlyColumns = false)
        {
            var csv = new DataTable();

            var hasIndexColumn = addIndexCol && !string.IsNullOrEmpty(indexColName);

            using (var reader = new CsvReader(textReader, true))
            {
                //columns
                if (hasIndexColumn)
                {
                    csv.Columns.Add(new DataColumn(indexColName));
                }

                var headers = reader.GetFieldHeaders();
                csv.Columns.AddRange(headers.Select(header => new DataColumn(header)).ToArray());

                if (!onlyColumns)
                {
                    //rows
                    int colsCount = reader.FieldCount;

                    while (reader.ReadNextRecord())
                    {
                        var row = csv.NewRow();

                        if (hasIndexColumn)
                        {
                            row[indexColName] = reader.CurrentRecordIndex + 1;
                        }

                        for (var i = 0; i < colsCount; i++)
                        {
                            row[headers[i]] = reader[i];
                        }

                        csv.Rows.Add(row);

                        if (maxRecords >= 0 && csv.Rows.Count == maxRecords)
                        {
                            break;
                        }
                    }
                }
            }

            return csv;
        }

        public static byte[] CsvExporterAsBytes<T>(this IEnumerable<T> results, string seperator = ",", params (string Header, Expression<Func<T, object>> Field)[] cols)
           where T : class
        {
            var csv = results.CsvExporterAsString(seperator, cols);
            return string.IsNullOrWhiteSpace(csv) ? new byte[] { } : Encoding.UTF8.GetBytes(csv);
        }

        public static string CsvExporterAsString<T>(this IEnumerable<T> results, string seperator = ",", params (string Header, Expression<Func<T, object>> Field)[] cols)
            where T : class
        {
            if (cols?.Length == 0)
            {
                return "";
            }

            var exporter = results.CsvExporter(seperator);
            foreach (var col in cols)
            {
                exporter = exporter.AddExportableColumn(col.Field, col.Header);
            }

            using var csvWriter = new StringWriter();
            exporter.ToCsv(csvWriter);

            return csvWriter.ToString();
        }

        public static CsvExporter<T> CsvExporter<T>(this IEnumerable<T> source, string seperator = ",") where T : class
        {
            return new CsvExporter<T>(source, seperator);
        }

    }
}
