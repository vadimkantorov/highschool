using System;
using System.Web.UI;

namespace Ne.Judge
{
	public partial class ErrorMessage : UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Visible = false;
		}

		public string Message
		{
			set
			{
				Visible = true;
				errLiteral.Text = value;
			}
		}
	}
}