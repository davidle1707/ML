using System;

namespace ML.Common.Log
{
	public interface ILogger
	{
		void Debug(string message);
		void Debug(string message, Exception exception);
		void DebugFormat(string message, params object[] args);

		void Info(string message);
		void Info(string message, Exception exception);
		void InfoFormat(string message, params object[] args);

		void Warn(string message);
		void Warn(string message, Exception exception);
		void WarnFormat(string message, params object[] args);

		void Error(string message);
		void Error(Exception message);
		void Error(string message, Exception exception);
		void ErrorFormat(string message, params object[] args);
		void ErrorFormat(string message, Exception exception, params object[] args);

		//void Error(LogbookEntryService entry);

		void Fatal(string message);
		void Fatal(string message, Exception exception);
		void FatalFormat(string message, params object[] args);

		bool IsDebugEnabled { get; }

		Func<string, string> OptimizeMessage { get; set; }

		// continue for all methods like Error, Fatal ...
	}
}
