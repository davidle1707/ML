using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace ML.Common
{
	public class WindowsImpersonationHelper
	{
		public string Domain { get; private set; }

		public string Password { get; private set; }

		public string UserName { get; private set; }

		private WindowsImpersonationContext _impersonationContext;

		public WindowsImpersonationHelper(string username, string password, string domain = "")
		{
			Domain = domain;
			Password = password;
			UserName = username;
		}

		public bool Impersonate()
		{
			var token = IntPtr.Zero;
			var tokenDuplicate = IntPtr.Zero;

			if (RevertToSelf()
				&& LogonUserA(UserName, Domain, Password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref token) != 0
				&& DuplicateToken(token, 2, ref tokenDuplicate) != 0)
			{
				_impersonationContext = new WindowsIdentity(tokenDuplicate).Impersonate();

				if (_impersonationContext != null)
				{
					CloseHandle(token);
					CloseHandle(tokenDuplicate);

					return true;
				}
			}

			if (token != IntPtr.Zero)
			{
				CloseHandle(token);
			}

			if (tokenDuplicate != IntPtr.Zero)
			{
				CloseHandle(tokenDuplicate);
			}

			return false;
		}

		public void Exit()
		{
			if (_impersonationContext != null)
			{
				_impersonationContext.Undo();
			}
		}

		#region Dll Imports

		const int LOGON32_LOGON_INTERACTIVE = 2;
		const int LOGON32_PROVIDER_DEFAULT = 0;

		[DllImport("advapi32.dll")]
		public static extern int LogonUserA(
			string lpszUserName,
			string lpszDomain,
			string lpszPassword,
			int dwLogonType,
			int dwLogonProvider,
			ref IntPtr phToken);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int DuplicateToken(
			IntPtr hToken,
			int impersonationLevel,
			ref IntPtr hNewToken);

		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool RevertToSelf();

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern bool CloseHandle(IntPtr handle);

		#endregion
	}
}
