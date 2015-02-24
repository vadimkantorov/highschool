using System;
using System.Web.Mvc;
using Model;
using NHibernate;
using Web.Extensions;

namespace Web.Services
{
	public class SubmissionOperationCheckAttribute : ActionFilterAttribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationContext filterContext)
		{
			var rawValue = filterContext.Controller.ValueProvider.GetValue(param).AttemptedValue;
			var id = Convert.ToInt32(rawValue);
			var submission = Session.Get<Submission>(id);

			if (!AuthorizationService.IsAllowed(UserSession.CurrentUser, submission, op.ToOperation()))
			{
				var expl = AuthorizationService.GetAuthorizationInformation(UserSession.CurrentUser, submission
					, op.ToOperation()).ToString();
				filterContext.Result = new UnauthorizedViewResult(op, expl);
			}
		}

		public SubmissionOperationCheckAttribute(SubmissionOperation op, string param = "id")
		{
			this.op = op;
			this.param = param;
		}

		public ISession Session { get; set; }

		public IUserSession UserSession { get; set; }

		public Rhino.Security.Interfaces.IAuthorizationService AuthorizationService { get; set; }

		readonly SubmissionOperation op;
		readonly string param;
	}
}