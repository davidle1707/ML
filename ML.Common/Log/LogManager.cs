using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using log4net.Config;

namespace ML.Common.Log
{
	public static class LogManager
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ILogger GetLogger(Type type = null, Func<string, string> optimizeMessage = null)
		{
			var safeType = type;

			if (safeType == null)
			{
				var frame = new StackTrace().GetFrame(1);
				safeType = frame.GetMethod().DeclaringType;
			}

			return new Log4NetWrapper(safeType, optimizeMessage);
		}

        public static ICollection Configure(string configPath, bool watch = true) 
			 => watch ? XmlConfigurator.ConfigureAndWatch(new FileInfo(configPath)) : XmlConfigurator.Configure(new FileInfo(configPath));
    }
}
