using System;
using System.Web.UI;

namespace NeJudge
{
	/// <summary>
	///		Summary description for QuestionMark.
	/// </summary>
	public class HelpMessage : UserControl
	{
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected System.Web.UI.WebControls.HyperLink qhref;

		private string _msg = "";
		private string uniq = Guid.NewGuid().ToString().Substring(0, 8);

		public string Message
		{
			get
			{
				return _msg;
			}
			set
			{
				_msg = value;
			}
		}

		private void Page_Load(object sender, EventArgs e)
		{
			Page.RegisterClientScriptBlock("message" + uniq,
				String.Format("<script>var msg{0}=\"{1}\"</script>", uniq, Server.HtmlEncode(_msg)));
			qhref.Attributes.Add("href", String.Format("javascript:alert(msg{0});", uniq));
		}
	}
}
