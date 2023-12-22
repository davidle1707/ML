using System;
using System.Threading.Tasks;
using log4net;
using Marklogic.Xcc;
using ML.Common;
using ML.Utils.MarkLogic.DataContext.Impl;

namespace ML.Utils.MarkLogic
{
    public class XDataContext
    {
        public string DbPath { get; }

        public string ConnectionString { get; }

        public bool LogQuery { get; set; }

        public bool ThrowException { get; set; }

        public RequestOptions RequestOptions { get; }

        private readonly ILog _log = LogManager.GetLogger(typeof(XDataContext));

        public XDataContext(string connectionString, string dbPath, bool logQueryString = true, bool throwExceptionIfError = false)
        {
            DbPath = dbPath;
            ConnectionString = connectionString;

            LogQuery = logQueryString;
            ThrowException = throwExceptionIfError;

            RequestOptions = new RequestOptions();
        }

        public XEntityNode<T, TId> Entity<T, TId>() where T : XEntity<TId> => new XEntityNodeImpl<T, TId>(this);

        #region Execute Async

        public async Task<string> ExecuteScalarAsync(string query)
        {
            var response = await ExecuteToObjectAsync<ExecuteScalarResponse>(query);

            return response.Value;
        }

        public async Task<T> ExecuteToObjectAsync<T>(string query, string separator = "\n", bool transaction = false) where T : class
        {
            var response = await ExecuteToStringAsync(query, separator, transaction);

            try
            {
                if (!string.IsNullOrWhiteSpace(response))
                {
                    return response.Deserialize<T>();
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
            }

            return Activator.CreateInstance<T>();
        }

        public async Task<string> ExecuteToStringAsync(string query, string separator = "\n", bool transaction = false)
        {
            var response = "";

            await ExecuteQueryAsync(query, transaction, sequence => response = sequence.AsString(separator));

            return response;
        }

        public Task<bool> ExecuteAsync(string query, bool transaction = false)
        {
            return ExecuteQueryAsync(query, transaction);
        }

        public Task<bool> ExecuteQueryAsync(string query, bool useTransaction, Action<ResultSequence> processResult = null)
        {
            return Task.Run(() => ExecuteQuery(query, useTransaction, processResult));
        }

        #endregion

        #region Execute

        private ContentSource _contentSource;

        public string ExecuteScalar(string query)
        {
            return ExecuteToObject<ExecuteScalarResponse>(query).Value;
        }

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
                _log.Error(ex.Message, ex);
            }

            return Activator.CreateInstance<T>();
        }

        public string ExecuteToString(string query, string separator = "\n", bool transaction = false)
        {
            var response = "";

            ExecuteQuery(query, transaction, sequence => response = sequence.AsString(separator));

            return response;
        }

        public bool Execute(string query, bool transaction = false)
        {
            return ExecuteQuery(query, transaction);
        }

        public bool ExecuteQuery(string query, bool useTransaction, Action<ResultSequence> processResult = null)
        {
            Session session = null;
            ResultSequence resultSequence = null;

            try
            {
                if (LogQuery)
                {
                    _log.Debug("(:Query:) " + query);
                }

                if (_contentSource == null)
                {
                    _contentSource = ContentSourceFactory.NewContentSource(new Uri(ConnectionString));
                }

                session = _contentSource.NewSession();

                if (useTransaction)
                {
                    session.TransactionMode = TransactionMode.Update;
                }

                var request = session.NewAdhocQuery(query, RequestOptions);
                resultSequence = session.SubmitRequest(request);

                if (useTransaction)
                {
                    session.Commit();
                }

                processResult?.Invoke(resultSequence);
            }
            catch (Exception ex)
            {
                if (!LogQuery)
                {
                    _log.Debug("(:Query:) " + query);
                }

                _log.Error(ex.Message, ex);

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
    }
}
