using log4net;
using Marklogic.Xcc;
using ML.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ML.Utils.MarkLogic
{
    public partial class MarkLogicManager
    {
        #region Property

        public string DbPath { get; private set; }

        public string ConnectionString { get; private set; }

        public RequestOptions RequestOptions { get; set; }

        public bool LogQuery { get; set; }

        public bool ThrowException { get; set; }

        #endregion

        #region Constructor

        private readonly ContentSource _contentSource;

        internal readonly ILog Log;

        public MarkLogicManager(string connectionString, string dbPath, Type logType = null)
        {
            Log = LogManager.GetLogger(logType ?? typeof(MarkLogicManager));

            ConnectionString = connectionString;
            DbPath = dbPath;

            _contentSource = ContentSourceFactory.NewContentSource(new Uri(ConnectionString));

            RequestOptions = new RequestOptions();
        }

        #endregion

        #region Map

        protected TDestination Map<TSource, TDestination>(TSource source)
        {
            return source.Map<TSource, TDestination>();
        }

        protected IEnumerable<TDestination> MapEnumerable<TSource, TDestination>(IEnumerable<TSource> sources)
        {
            return sources.MapEnumerable<TSource, TDestination>();
        }

        protected List<TDestination> MapList<TDestination>(IList sources)
        {
            return sources.MapList<TDestination>();
        }

        #endregion

        #region ExecuteFunction

        public T ExecuteFuncToObject<T>(string funcName, bool transaction, params object[] paramValues) where T : class
        {
            try
            {
                var response = ExecuteFuncToString(funcName, transaction, paramValues);

                if (LogQuery)
                {
                    Log.Debug("XQueryResponse: " + response);
                }

                if (string.IsNullOrEmpty(response))
                {
                    return Activator.CreateInstance<T>();
                }

                return response.Deserialize<T>();
            }
            catch (Exception ex)
            {
                Log.Error(ex);

                return Activator.CreateInstance<T>();
            }
        }

        public string ExecuteFuncToString(string funcName, bool transaction, params object[] paramValues)
        {
            var response = "";

            var query = new StringBuilder();
            query.AppendLine(XUtils.DeclareFunction(DbPath, funcName));
            query.AppendLine(XUtils.GetFunction(DbPath, funcName, paramValues));

            HandleExecuting(query.ToString(), transaction, sequence => response = sequence.AsString("\n"));

            return response;
        }

        #endregion

        #region ExecuteFile

        /// <summary>
        /// @params: key (parameter name, need not include '@') - value (parameter value)
        /// </summary>
        protected T ExecuteFileToObject<T>(string filePath, Dictionary<string, string> @params = null) where T : class
        {
            var query = XBuilder.FileHelper().ToQuery(filePath, @params);

            return ExecuteToObject<T>(query);
        }

        /// <summary>
        /// @params: key (parameter name, need not include '@') - value (parameter value, use XQueryHelper<TEntity>().ParamValue to convert value)
        /// </summary>
        protected T ExecuteFileToObject<T>(string filePath, Dictionary<string, object> @params = null) where T : class
        {
            var query = XBuilder.FileHelper().ToQuery(filePath, @params);

            return ExecuteToObject<T>(query);
        }

        protected T ExecuteFileToObject<T>(string filePath, params string[] paramValues) where T : class
        {
            var query = XBuilder.FileHelper().ToQuery(filePath, paramValues);

            return ExecuteToObject<T>(query);
        }

        /// <summary>
        /// use XQueryHelper<TEntity>().ParamValue to convert value
        /// </summary>
        protected T ExecuteFileToObject<T>(string filePath, params object[] paramValues) where T : class
        {
            var query = XBuilder.FileHelper().ToQuery(filePath, paramValues);

            return ExecuteToObject<T>(query);
        }

        #endregion

        #region Execute

        public T ExecuteToObject<T>(string query, string separator = "\n", bool transaction = false) where T : class
        {
            var response = ExecuteToString(query, separator, transaction);

            try
            {
                if (!string.IsNullOrWhiteSpace(response))
                {
                    return response.Deserialize<T>();

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }

            return Activator.CreateInstance<T>();
        }

        public string ExecuteToString(string query, string separator = "\n", bool transaction = false)
        {
            var response = "";

            HandleExecuting(query, transaction, sequence => response = sequence.AsString(separator));

            return response;
        }

        public bool Execute(string query, bool transaction = false)
        {
            return HandleExecuting(query, transaction, null);
        }

        private bool HandleExecuting(string query, bool useTransaction, Action<ResultSequence> processResponse)
        {
            Session session = null;
            ResultSequence resultSequence = null;

            try
            {
                if (LogQuery)
                {
                    Log.Debug("(:XQueryRequest:) " + query);
                }

                session = _contentSource.NewSession();

                if (useTransaction)
                {
                    session.TransactionMode = TransactionMode.Update;
                }

                var request = session.NewAdhocQuery(null);
                request.Query = query;
                request.Options = RequestOptions;

                resultSequence = session.SubmitRequest(request);

                if (useTransaction)
                {
                    session.Commit();
                }

                processResponse?.Invoke(resultSequence);
            }
            catch (Exception ex)
            {
                if (!LogQuery)
                {
                    Log.Debug("(:XQueryRequest:) " + query);
                }

                Log.Error(ex.Message, ex);

                if (session != null && useTransaction)
                {
                    session.Rollback();
                }

                if (ThrowException)
                {
                    throw new XException(ex.Message, ex);
                }

                return false;
            }
            finally
            {
                resultSequence?.Close();
                session?.Close();

            }

            return true;
        }

        #endregion

        private XNodeFunc<TEntity> Query<TEntity>() => XBuilder<TEntity>.NodeFunc;

    }
}
