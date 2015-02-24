using System;
using System.Web.UI;

namespace Ne.Judge
{
	public partial class HelpMessage : UserControl
	{
		private string _msg = "";
		
		public string Message
		{
			get { return _msg; }
			set { _msg = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			qhref.Attributes.Add("href",String.Format("javascript:alert('{0}')",Server.HtmlEncode(_msg)));
		}
	}
}