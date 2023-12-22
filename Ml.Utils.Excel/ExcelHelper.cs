using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace ML.Utils.Excel
{
    public static partial class ExcelHelper
    {
        public static List<string> GetColumnsInFile(string filePath, string fileExt = "")
        {
            var table = GetImportData(filePath, 1, fileExt);

            return (from DataColumn column in table.Columns select column.ColumnName).ToList();
        }

        public static DataTable GetImportData(string filePath, int maxRow = -1, string fileExt = "")
        {
            var data = new DataTable();

            var version = GetVersion(filePath, fileExt);
            if (version != null)
            {
                switch (version.Value)
                {
                    case ExcelVersion.CSV:
                        data = CsvHelper.ToDataTable(filePath, maxRow, false);
                        break;

                    case ExcelVersion.Excel2003:
                    case ExcelVersion.Excel2007:
                    case ExcelVersion.Excel2010:
                        data = ToDataTable(filePath, version.Value, maxRow);
                        break;
                }
            }

            return data;
        }

        public static DataTable ToDataTable(string filePath, ExcelVersion version, int maxRecords = -1) => Execute(version, filePath, connection =>
        {
            var oData = new DataTable();

            var sheets = new List<string>();
            var scheme = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (scheme != null && scheme.Rows.Count > 0)
            {
                sheets.AddRange(from DataRow sh in scheme.Rows select sh["table_name"].ToString());

                var from = !string.IsNullOrEmpty(sheets[0]) ? sheets[0] : "Sheet1";
                var top = maxRecords > -1 ? $" top {maxRecords + 1}" : string.Empty;

                var adapter = new OleDbDataAdapter($"select{top} * from [{from}];", connection);
                adapter.Fill(oData);
            }

            return oData;
        });

        public static DataTable ToDataTable(ExcelVersion version, string filePath, string sheetName, int maxRecords = -1) => Execute(version, filePath, connection =>
        {
            var query = $"select{(maxRecords > -1 ? $" top {maxRecords + 1}" : string.Empty)} * from [{sheetName}];";
            var dt = new DataTable();

            var adapter = new OleDbDataAdapter(query, connection);
            adapter.Fill(dt);

            return dt;
        });

        public static List<ExcelSheetInfo> GetSheets(string filePath, string fileExt = "")
        {
            var version = GetVersion(filePath, fileExt);
            return version != null ? GetSheets(version.Value, filePath) : new List<ExcelSheetInfo>();
        }

        public static List<ExcelSheetInfo> GetSheets(ExcelVersion version, string filePath)
        {
            var sheets = new List<ExcelSheetInfo>();

            switch (version)
            {
                case ExcelVersion.CSV:
                    sheets.Add(CsvHelper.GetSheet(filePath));
                    break;

                case ExcelVersion.Excel2003:
                case ExcelVersion.Excel2007:
                case ExcelVersion.Excel2010:
                    sheets.AddRange(ProcesGetSheets(version, filePath));
                    break;
            }

            return sheets;
        }

        public static (DataSet Data, Exception Error) ToDataSet(ExcelVersion version, string filePath) => Execute<(DataSet, Exception)>(version, filePath, connection =>
        {
            var scheme = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (scheme == null || scheme.Rows.Count == 0)
            {
                return (null, new Exception("Cannot get schema Tables."));
            }

            var ds = new DataSet();

            foreach (DataRow dr in scheme.Rows)
            {
                var sheetName = dr["table_name"].ToStr();
                if (string.IsNullOrWhiteSpace(sheetName))
                {
                    continue;
                }

                try
                {
                    var adapter = new OleDbDataAdapter($"select * from [{sheetName}];", connection);
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    dt.TableName = sheetName;
                    ds.Tables.Add(dt);
                }
                catch (Exception ex)
                {
                }
            }

            return (ds, null);
        });

        public static Exception UpdateDatatable(ExcelVersion version, string filePath, string sheetName, DataTable dtSheetData) => Execute<Exception>(version, filePath, connection =>
        {
            try
            {
                var adapter = new OleDbDataAdapter($"select * from [{sheetName}];", connection);

                var builder = new OleDbCommandBuilder(adapter);
                adapter.InsertCommand = builder.GetInsertCommand();

                adapter.Update(dtSheetData);

                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        });

        #region Process

        private static List<ExcelSheetInfo> ProcesGetSheets(ExcelVersion version, string filePath) => Execute(version, filePath, connection =>
        {
            var sheets = new List<ExcelSheetInfo>();

            var scheme = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (scheme != null && scheme.Rows.Count > 0)
            {
                foreach (DataRow dr in scheme.Rows)
                {
                    var sheetName = dr["table_name"].ToStr();
                    if (string.IsNullOrWhiteSpace(sheetName))
                    {
                        continue;
                    }

                    var sheet = new ExcelSheetInfo { Name = sheetName };

                    try
                    {
                        var dt = new DataTable();
                        var adapter = new OleDbDataAdapter($"select top 1 * from [{sheetName}];", connection);
                        adapter.Fill(dt);
                        sheet.Columns.AddRange(from DataColumn column in dt.Columns select column.ColumnName);
                    }
                    catch (Exception ex)
                    {
                        sheet.Exception = ex;
                    }

                    sheets.Add(sheet);
                }
            }

            return sheets;
        });


        private static TResult Execute<TResult>(ExcelVersion version, string filePath, Func<OleDbConnection, TResult> execute)
        {
            using (var connect = new OleDbConnection(GetConnectionString(version, filePath)))
            {
                try
                {
                    connect.Open();

                    return execute(connect);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (connect.State == ConnectionState.Open)
                    {
                        connect.Close();
                    }
                }
            }
        }

        private static ExcelVersion? GetVersion(string filePath, string fileExt = "")
        {
            fileExt = (string.IsNullOrWhiteSpace(fileExt) ? Path.GetExtension(filePath) : fileExt) ?? string.Empty;

            switch (fileExt.ToLower())
            {
                case ".csv":
                case "csv":
                    return ExcelVersion.CSV;

                case ".xls":
                case "xls":
                    return ExcelVersion.Excel2003;

                case ".xlsx":
                case "xlsx":
                    return ExcelVersion.Excel2007;
            }

            return null;
        }

        private static string GetConnectionString(ExcelVersion version, string filePath)
        {
            switch (version)
            {
                case ExcelVersion.Excel2003:
                    return $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;MAXSCANROWS=15;READONLY=FALSE\"";

                case ExcelVersion.Excel2007:
                case ExcelVersion.Excel2010:
                default:
                    return $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=15;READONLY=FALSE\"";
            }
        }

        private static string ToStr(this object source, string defVal = "")
        {
            if (source == null || source.Equals(DBNull.Value))
            {
                return defVal;
            }

            return source.ToString();
        }

        #endregion

    }
}
