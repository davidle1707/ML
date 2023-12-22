using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace ML.Common
{
    public static class DataTableHelper
    {
        /// <summary>
        /// expression: sql query
        /// defObjEachRow: each row will contains props (name/value) of this object.
        /// </summary>
        public static TableToObjectResult<T> FilterObjects<T>(IEnumerable<T> sources, string expression = "", string sort = "", object extendObj = null)
        {
            var resultTable = ToTable(sources, extendObj: extendObj);
            if (resultTable.Error != null)
            {
                return new TableToObjectResult<T> { Error = resultTable.Error };
            }

            return ToObjects(resultTable, expression, sort);
        }

        /// <summary>
        /// defObjEachRow -> each row will contains props (name/value) of this object.
        /// </summary>
        public static ObjectToTableResult<T> ToTable<T>(IEnumerable<T> objects, string[] ignorePropNames = null, object extendObj = null)
        {
            var props = typeof(T).GetProperties().Where(a => IsPropReadable(a) && (ignorePropNames == null || !ignorePropNames.Contains(a.Name))).ToList();
            if (props.Count == 0)
            {
                return new ObjectToTableResult<T> { Error = new Exception("Cannot fetch the list of readable properties.") };
            }

            var result = new ObjectToTableResult<T>
            {
                Table = new DataTable(),
                PrimaryKeyName = "__Id__",
                MapObjects = new List<(Guid Key, T Source)>()
            };

            try
            {
                // columns
                result.Table.Columns.Add(result.PrimaryKeyName, typeof(Guid));

                foreach (var prop in props)
                {
                    result.Table.Columns.Add(prop.Name, SafeTypeForTable(prop.PropertyType));
                }

                var extendRow = ParseObject(extendObj);
                foreach (var item in extendRow)
                {
                    result.Table.Columns.Add(item.Name, SafeTypeForTable(item.Type));
                }

                // rows
                foreach (var obj in objects)
                {
                    var primaryKey = Guid.NewGuid();

                    var dr = result.Table.NewRow();
                    dr[result.PrimaryKeyName] = primaryKey;

                    foreach (var prop in props)
                    {
                        var propValue = prop.GetValue(obj);
                        dr[prop.Name] = propValue ?? DBNull.Value;
                    }

                    foreach (var item in extendRow)
                    {
                        dr[item.Name] = item.Value ?? DBNull.Value;
                    }

                    result.Table.Rows.Add(dr);
                    result.MapObjects.Add((primaryKey, obj));
                }
            }
            catch (Exception ex)
            {
                result.Error = ex;
            }

            return result;
        }

        public static ObjectToTableResult<T> ToTableSchema<T>(string[] ignorePropNames = null)
        {
            var props = typeof(T).GetProperties().Where(a => IsPropReadable(a) && (ignorePropNames == null || !ignorePropNames.Contains(a.Name))).ToList();
            if (props.Count == 0)
            {
                return new ObjectToTableResult<T> { Error = new Exception("Cannot fetch the list of readable properties.") };
            }

            var result = new ObjectToTableResult<T>
            {
                Table = new DataTable()
            };

            try
            {
                foreach (var prop in props)
                {
                    result.Table.Columns.Add(prop.Name, SafeTypeForTable(prop.PropertyType));
                }
            }
            catch (Exception ex)
            {
                result.Error = ex;
            }

            return result;
        }

        public static TableToObjectResult<T> ToObjects<T>(ObjectToTableResult<T> tableResult, string expression = "", string sort = "")
        {
            var objects = new List<T>();

            var exception = ProcessFilterTable(tableResult.Table, expression, drv =>
            {
                var key = (Guid)drv[tableResult.PrimaryKeyName];
                objects.Add(tableResult.MapObjects.FirstOrDefault(a => a.Key == key).Object);
            }, sort);

            objects.RemoveAll(a => a == null);

            return new TableToObjectResult<T>
            {
                Error = exception,
                Objects = objects
            };
        }

        public static bool CheckExpression(DataTable source, string expression, bool throwIfError = false)
        {
            try
            {
                var dv = new DataView(source) { RowFilter = expression };
                return dv.Count > 0;
            }
            catch (Exception ex)
            {
                if (throwIfError)
                {
                    throw ex;
                }

                return false;
            }
        }

        public static Exception ProcessFilterTable(DataTable dt, string expression, Action<DataRowView> process, string sort = "")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(expression) && string.IsNullOrWhiteSpace(sort))
                {
                    return new Exception("Payload is invalid.");
                }

                var dv = new DataView(dt);
                if (!string.IsNullOrWhiteSpace(expression)) dv.RowFilter = expression;
                if (!string.IsNullOrWhiteSpace(sort)) dv.Sort = sort;

                foreach (DataRowView drv in dv)
                {
                    process(drv);
                }

                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        private static List<(string Name, Type Type, object Value)> ParseObject(object source)
        {
            var infos = new List<(string Name, Type Type, object Value)>();

            if (source != null)
            {
                try
                {
                    foreach (var prop in source.GetType().GetProperties().Where(a => a.CanRead && IsPropReadable(a)))
                    {
                        var propValue = prop.GetValue(source);
                        infos.Add((prop.Name, prop.PropertyType, propValue));
                    }
                }
                catch { }
            }

            return infos;
        }

        private static bool IsPropReadable(PropertyInfo propInfo) 
            => propInfo.CanRead && propInfo.PropertyType.IsSimpleType();

        private static Type SafeTypeForTable(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return Nullable.GetUnderlyingType(type);
            }

            return type;
        }

        public class TableToObjectResult<T>
        {
            public Exception Error { get; set; }

            public List<T> Objects { get; set; }
        }

        public class ObjectToTableResult<T>
        {
            public Exception Error { get; set; }

            public DataTable Table { get; set; }

            public string PrimaryKeyName { get; set; }

            public List<(Guid Key, T Object)> MapObjects { get; set; }
        }
    }
}
