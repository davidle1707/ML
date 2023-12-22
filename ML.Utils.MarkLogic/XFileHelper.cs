using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ML.Utils.MarkLogic
{
    public sealed class XFileHelper
    {
        private string _rootPath;

        internal XFileHelper(string extendRootPath = "XQuery")
        {
            SetRootPath(extendRootPath);
        }

        public void SetRootPath(string extendRootPath = "XQuery")
        {
            _rootPath = !string.IsNullOrWhiteSpace(extendRootPath)
                ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, extendRootPath)
                : AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// @params: key (parameter name, need not include '@') - value (parameter value)
        /// </summary>
        public string ToQuery(string filePath, Dictionary<string, string> @params = null)
        {
            var query = ReadContent(filePath);

            if (!string.IsNullOrWhiteSpace(query) && @params != null)
            {
                foreach (var param in @params.Where(p => !string.IsNullOrWhiteSpace(p.Key)))
                {
                    var paramName = !param.Key.StartsWith("@") ? "@" + param.Key : param.Key;
                    query = query.Replace(paramName, param.Value);
                }
            }

            return query;
        }

        /// <summary>
        /// @params: key (parameter name, need not include '@') - value (parameter value)
        /// </summary>
        public string ToQuery(string filePath, Dictionary<string, object> @params = null)
        {
            var query = ReadContent(filePath);

            if (string.IsNullOrWhiteSpace(query) || @params == null)
            {
                return query;
            }

            foreach (var param in @params.Where(p => !string.IsNullOrWhiteSpace(p.Key)))
            {
                var paramName = !param.Key.StartsWith("@") ? "@" + param.Key : param.Key;
                var paramValue = XUtils.ParamValue(param.Value);

                query = query.Replace(paramName, paramValue);
            }

            return query;
        }

        public string ToQuery(string filePath, params string[] paramValues)
        {
            var query = ReadContent(filePath);

            if (paramValues != null && paramValues.Length > 0)
            {
                var @params = Regex.Matches(query, @"\@\w+");

                for (var i = 0; i < @params.Count; i++)
                {
                    if (i >= paramValues.Length)
                    {
                        break;
                    }


                    query = query.Replace(@params[i].Value, paramValues[i]);
                }
            }

            return query;
        }

        public string ToQuery(string filePath, params object[] paramValues)
        {
            var query = ReadContent(filePath);

            if (paramValues != null && paramValues.Length > 0)
            {
                var @params = Regex.Matches(query, @"\@\w+");

                for (var i = 0; i < @params.Count; i++)
                {
                    if (i >= paramValues.Length)
                    {
                        break;
                    }

                    var paramValue = XUtils.ParamValue(paramValues[i]);
                    query = query.Replace(@params[i].Value, paramValue);
                }
            }

            return query;
        }

        private string ReadContent(string filePath)
        {
            var fullFilePath = Path.Combine(_rootPath, filePath);

            return File.Exists(fullFilePath) ? File.ReadAllText(fullFilePath) : string.Empty;
        }

    }
}
