using System;
using System.Runtime.InteropServices;

namespace Tester.Runner
{
	class PinnedObject : IDisposable
	{
		public PinnedObject(object obj)
		{
			gcHandle = GCHandle.Alloc(obj, GCHandleType.Pinned);
		}

		public void Dispose()
		{
			gcHandle.Free();
		}

		public IntPtr Address { get { return gcHandle.AddrOfPinnedObject(); }}

		private GCHandle gcHandle;
	}
}