using System;
using log4net;

namespace ML.Common.Log
{
	public class Log4NetWrapper : ILogger
	{
		private readonly ILog _logger;

        public Func<string, string> OptimizeMessage { get; set; }

        public Log4NetWrapper(Type type = null, Func<string, string> optimizeMessage = null)
		{
			_logger = type != null ? log4net.LogManager.GetLogger(type) : log4net.LogManager.GetLogger("Unknown");
			OptimizeMessage = optimizeMessage;
		}

		public void Debug(string message)
		{
			_logger.Debug(FormatMessage(message));
		}

		public void Debug(string message, Exception exception)
		{
			_logger.Debug(FormatMessage(message), exception);
		}

		public void DebugFormat(string message, params object[] args)
		{
			_logger.DebugFormat(FormatMessage(message), args);
		}

		public void Info(string message)
		{
			_logger.Info(FormatMessage(message));
		}

		public void Info(string message, Exception exception)
		{
			_logger.Info(message, exception);
		}

		public void InfoFormat(string message, params object[] args)
		{
			_logger.InfoFormat(FormatMessage(message), args);
		}

		public void Warn(string message)
		{
			_logger.Warn(FormatMessage(message));
		}

		public void Warn(string message, Exception exception)
		{
			_logger.Warn(FormatMessage(message), exception);
		}

		public void WarnFormat(string message, params object[] args)
		{
			_logger.WarnFormat(FormatMessage(message), args);
		}

		public void Error(string message)
		{
			_logger.Error(FormatMessage(message));
		}

		public void Error(Exception exception)
		{
			_logger.Error(exception);
		}

		public void Error(string message, Exception exception)
		{
			_logger.Error(FormatMessage(message), exception);
		}

		public void ErrorFormat(string message, Exception exception, params object[] args)
		{
			var msg = string.Format(FormatMessage(message), args);
			_logger.Error(msg, exception);
		}

		public void ErrorFormat(string message, params object[] args)
		{
			_logger.ErrorFormat(FormatMessage(message), args);
		}

		public void Fatal(string message)
		{
			_logger.Fatal(FormatMessage(message));
		}

		public void Fatal(string message, Exception exception)
		{
			_logger.Fatal(FormatMessage(message), exception);
		}

		public void FatalFormat(string message, params object[] args)
		{
			_logger.FatalFormat(FormatMessage(message), args);
		}

		public bool IsDebugEnabled
		{
			get { return _logger.IsDebugEnabled; }
		}

		private string FormatMessage(string message) => OptimizeMessage?.Invoke(message) ?? message;
	}
}
