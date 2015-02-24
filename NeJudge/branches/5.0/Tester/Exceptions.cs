using System;
using System.ComponentModel;
using Microsoft.Win32;

namespace Tester
{
	public class FailedActionException : Exception
	{
		public FailedActionException(string failedAction, string detailedError)
			: base(FormatError(failedAction, detailedError))
		{
		}

		private static string FormatError(string failedAction, string detailedError)
		{
			return string.Format("Не удалось {0}. {1}.", failedAction, detailedError);
		}
	}

	public class Win32ExceptionWithContext : FailedActionException
	{
		public Win32ExceptionWithContext(string failedAction)
			: base(failedAction, FormatDetailedError())
		{
		}

		private static string FormatDetailedError()
		{
			var win32Error = new Win32Exception();
			return string.Format("{0} (код ошибки: {1})", win32Error.Message, win32Error.NativeErrorCode);
		}
	}

	public class RegistryKeyNotFoundException : FailedActionException
	{
		public RegistryKeyNotFoundException(string failedAction, RegistryKey root, string subKey, string name)
			: base(failedAction, FormatDetailedError(root, subKey, name))
		{
		}

		private static string FormatDetailedError(RegistryKey root, string subKey, string name)
		{
			return string.Format(@"Не найден ключ реестра {0}\{1}\{2}", root, subKey, name);
		}
	}
}