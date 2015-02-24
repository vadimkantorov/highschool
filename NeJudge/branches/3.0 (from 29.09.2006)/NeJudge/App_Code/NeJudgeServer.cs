using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Ne.Interfaces;

namespace Ne.Helpers
{
	public static class NeJudgeServer
	{
		static INeJudgeServer instance = null;
		static readonly string uri = ConfigurationManager.AppSettings["NeJudgeServerUri"];
		static object sync = new object();

		public static INeJudgeServer Instance
		{
			get
			{
				if ( instance == null )
					lock ( sync )
						instance = (INeJudgeServer) Activator.GetObject(typeof(INeJudgeServer), uri);
				return instance;
			}
		}
	}
}
