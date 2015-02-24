using System;
using System.Runtime.InteropServices;

namespace Tester.Runner
{
	static class SafeHandleExtensions
	{
		public static void EnsureIsValid(this SafeHandle handle, string message)
		{
			if (handle.IsInvalid)
				throw new Win32ExceptionWithContext(message);
		}

		public static readonly IntPtr InvalidHandleValue = new IntPtr(-1);
	}
}