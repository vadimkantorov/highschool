using System;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Ne.Judge
{
	public partial class ErrorPage : Page
	{
		private void DisplayDetailedError()
		{
			Exception e = (Exception) Context.Items["Error"];
			string pattern = "<H2 style='FONT-SIZE: 14pt; COLOR: red'>{0}</H2>{1}";

			if ( e is NeJudgeSecurityException )
			{
				panelDetailedError.Visible = false;
				mesLiteral.Text = String.Format(pattern, @"Недостаточно прав.",
				                                "Вы не обладаете правами группы " + e.Message); //+"\r\n"+"Ваши права: "+groups;
			}
			else if ( e is NeJudgeInvalidParametersException )
			{
				panelDetailedError.Visible = false;
				mesLiteral.Text = String.Format(pattern, @"Были указаны неверные параметры.", e.Message);
			}
			else
			{
				litMessage.Text = e.Message;
				litSource.Text = e.Source;
				litStackTrace.Text = Regex.Replace(e.StackTrace, "\n", "<br />");
				litErrorDate.Text = e.Data["Time"].ToString();
				litErrorType.Text = e.GetType().FullName;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			DisplayDetailedError();
		}
	}
}