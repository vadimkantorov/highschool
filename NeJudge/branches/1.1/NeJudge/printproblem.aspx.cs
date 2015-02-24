using System;
using System.Web.UI;

namespace Ne.Judge
{
	/// <summary>
	/// Summary description for printproblem1.
	/// </summary>
	public class printproblem1 : Page
	{
		protected printproblem Print;

		private void Page_Load(object sender, EventArgs e)
		{
			try
			{
				Print.PID = int.Parse(Page.Request.QueryString["PID"]);
			}
			catch
			{
				Print.PID = -1;
			}
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion
	}
}