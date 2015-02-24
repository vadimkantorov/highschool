using System;
using System.Collections.Generic;
using System.Text;

using Ne.ContestTypeHandlers;

namespace ICPCHandler
{
	public class ICPCTesterManager : ITesterManager
	{
		public Ne.Tester.Tester CreateTester()
		{
			return new ICPCTester();
		}
	}
}
