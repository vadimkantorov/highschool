using System.Web.Mvc;
using Model;

namespace Web.Extensions
{
	public class UnauthorizedViewResult : ViewResult
	{
		public UnauthorizedViewResult(object op, string explanation)
		{
			ViewName = "AuthorizationFailure";
			ViewData = new ViewDataDictionary(AuthorizationResult.Denied(op, explanation));
		}
	}
}