using System.Web.Mvc;
using Model;
using Web.Extensions;
using IAuthorizationService = Rhino.Security.Interfaces.IAuthorizationService;

namespace Web.Services
{
	public class SystemOperationCheckAttribute  : ActionFilterAttribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationContext filterContext)
		{
			if (!AuthorizationService.IsAllowed(UserSession.CurrentUser, op.ToOperation()))
			{
				var expl = AuthorizationService.GetAuthorizationInformation(UserSession.CurrentUser, op.ToOperation()).ToString();
				filterContext.Result = new UnauthorizedViewResult(op, expl);
			}
		}

		public SystemOperationCheckAttribute(SystemOperation op)
		{
			this.op = op;
		}

		public IUserSession UserSession { get; set; }
		public IAuthorizationService AuthorizationService { get; set; }

		readonly SystemOperation op;
	}
}