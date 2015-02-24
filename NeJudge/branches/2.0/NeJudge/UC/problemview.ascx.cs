using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Ne.Database.Classes;
namespace Ne.Judge
{
	public partial class ProblemView : System.Web.UI.UserControl
	{
		int problemID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				Problem p = Problem.GetProblem(problemID);
				nameLiteral.Text = String.Format("<h1>Задача {0} ( #{1} ). {2}</h1>", p.ShortName, p.ID, p.Name);
				tlLiteral.Text += (p.TimeLimit / 1000).ToString() + " секунды";
				mlLiteral.Text += (p.MemoryLimit / 1024).ToString() + " КБайт";
				olLiteral.Text += p.OutputLimit + " байт";
				if (p.InputFile == Problem.STDIN_NAME)
					ifLiteral.Text += "Стандартный поток ввода";
				else
					ifLiteral.Text += p.InputFile;
				if (p.OutputFile == Problem.STDOUT_NAME)
					ofLiteral.Text += "Стандартный поток вывода";
				else
					ofLiteral.Text += p.OutputFile;
				p.LoadStatement();
				problemXml.Document = p.Statement.XmlDocument;
			}
		}

		public int ProblemID
		{
			set { problemID = value; }
		}
	}
}