using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ne.Judge.ErrorIO;

namespace Ne.Judge
{
	/// <summary>
	///		Summary description for errorpage.
	/// </summary>
	public class errorpage : UserControl
	{
		protected Panel panelDetailedError;
		protected Literal mesLiteral;
		protected System.Web.UI.WebControls.Literal litErrorDate;
		protected System.Web.UI.WebControls.Literal litMessage;
		protected System.Web.UI.WebControls.Literal litSource;
		protected System.Web.UI.WebControls.Literal litErrorType;
		protected System.Web.UI.WebControls.Literal litStackTrace;
		
		private bool DisplayDetailedError()
		{
			Trace.Write("myErrorPage.aspx", "Display Detailed Error");

			// The error is stored with whatever method is specific in web.config in the tag:
			// <add key="customErrorMethod" value="application/context/cookie/querystring/off" />
			IErrorIOHandler objErrorBasket = ErrorIOFactory.Create(ConfigurationSettings.AppSettings["customErrorMethod"]);
			ErrorStorage st = objErrorBasket.Retrieve();
			// Whatever the method used, always clear it when done.
			objErrorBasket.Clear();

			string pattern = "<H2 style='FONT-SIZE: 14pt; COLOR: red'>{0}</H2>{1}";
			if ( st.Type == typeof(NeJudgeSecurityException).FullName )
			{
				panelDetailedError.Visible = false;
				mesLiteral.Text = String.Format(pattern, @"Недостаточно прав.",
					"Вы не обладаете правами группы " + st.Message); //+"\r\n"+"Ваши права: "+groups;

			}
			else if ( st.Type == typeof(NeJudgeInvalidParametersException).FullName )
			{
				panelDetailedError.Visible = false;
				mesLiteral.Text = String.Format(pattern, @"Были указаны неверные параметры.", 
					@"В качестве параметра " + st.Message + @" было передано неверное значение.");
			}
			else
			{
				st.StackTrace = Regex.Replace(st.StackTrace, "\n", "<br />");
				litMessage.Text = st.Message;
				litSource.Text = st.Source;
				litStackTrace.Text = st.StackTrace;
				litErrorDate.Text = st.Date;
				litErrorType.Text = st.Type;
			}
			return true;
		}

		private void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["code"] == null)
			{
				DisplayDetailedError();
				Server.ClearError();
				/*StringBuilder message = new StringBuilder(); 
				if (Server != null) 
				{
					Exception ex;
					for (ex = Server.GetLastError(); ex != null; ex = ex.InnerException) 
					{
						message.AppendFormat("{0}: {1}{2}\r\n", 
							ex.GetType().FullName, 
							ex.Message,
							ex.StackTrace);
					}
					LiteralControl l = new LiteralControl(message.ToString());
					Controls.Add(l);

					Server.ClearError();
				}*/
			}
			else
			{
				int code;
				try
				{
					code = int.Parse(Request.QueryString["code"]);
				}
				catch
				{
					code = -1;
				}
				switch (code)
				{
					case 404:
						break;
					default:
						break;
				}
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}

		#endregion
	}
}