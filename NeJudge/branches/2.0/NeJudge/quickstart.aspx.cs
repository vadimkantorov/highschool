using System;
using System.Web.UI;
using Ne.Database.Classes;

public partial class quickstart : Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		Problem p = new Problem(8, "Ёлектронные часы", 2000, 64 * 1024 * 1024, 1000000, 
			Problem.STDIN_NAME, Problem.STDOUT_NAME);
		p.Store();
	}
}