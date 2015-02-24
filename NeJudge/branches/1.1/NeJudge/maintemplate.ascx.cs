using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ne.Judge
{
	/// <summary>
	///		Summary description for maintemplate.
	/// </summary>
	public class maintemplate : UserControl
	{
		protected PlaceHolder Content;
		protected header Header1;
		protected string _pagename;
		protected string _content;

		private void Page_Init(object sender, EventArgs e)
		{
			Header1.PageName = _pagename;
			Control c = LoadControl(ContentPath);
			//c.ID="FuckingControl";
			Content.Controls.Add(c);
		}

		public string PageName
		{
			get { return _pagename; }
			set { _pagename = value; }
		}

		public string ContentPath
		{
			get { return _content; }
			set { _content = value; }
		}

		#region Web Form Designer generated code

		protected override void OnInit(EventArgs e)
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
			this.Init += new EventHandler(this.Page_Init);
		}

		#endregion
	}
}