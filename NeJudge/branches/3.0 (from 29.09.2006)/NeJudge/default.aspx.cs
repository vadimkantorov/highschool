using System;
using System.Web.UI;

public partial class DefaultPage : Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if ( Page.User.IsInRole("Anonymous") )
		{
			registeredBanner.Visible = false;
			anonymousBanner.Visible = true;
		}
		else
		{
			registeredBanner.Visible = true;
			anonymousBanner.Visible = false;
		}
	}
}