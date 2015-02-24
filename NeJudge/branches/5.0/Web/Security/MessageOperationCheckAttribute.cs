using System;
using System.Web.Mvc;
using Model;
using NHibernate;
using Web.Extensions;

namespace Web.Services
{
	public class MessageOperationCheckAttribute : ActionFilterAttribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationContext filterContext)
		{
			var rawValue = filterContext.Controller.ValueProvider.GetValue(param).AttemptedValue;
			var id = Convert.ToInt32(rawValue);
			var Message = Session.Get<Message>(id);

			if (!AuthorizationService.IsAllowed(UserSession.CurrentUser, Message, op.ToOperation()))
			{
				var expl = AuthorizationService.GetAuthorizationInformation(UserSession.CurrentUser, Message
				                                                            , op.ToOperation()).ToString();
				filterContext.Result = new UnauthorizedViewResult(op, expl);
			}
		}

		public MessageOperationCheckAttribute(MessageOperation op, string param = "id")
		{
			this.op = op;
			this.param = param;
		}

		public ISession Session { get; set; }

		public IUserSession UserSession { get; set; }

		public Rhino.Security.Interfaces.IAuthorizationService AuthorizationService { get; set; }

		readonly MessageOperation op;
		readonly string param;
	}
}