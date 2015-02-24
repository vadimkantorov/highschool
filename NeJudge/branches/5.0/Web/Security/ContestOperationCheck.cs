using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DataAccess.Queries.TestInfos;
using Model;
using NHibernate;
using Web.Extensions;
using IAuthorizationService = Rhino.Security.Interfaces.IAuthorizationService;

namespace Web.Services
{
	public enum LookFor
	{
		SmartAss,
		Contest,
		Problem,
		Test,
		TestInfo,
		Message,
		Submission
	}

	public class ContestOperationCheckAttribute : ActionFilterAttribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationContext filterContext)
		{
			if(lookFor == LookFor.SmartAss)
			{
				lookFor = LookFor.Contest;
				foreach(var kvp in smartAssLookUp)
				{
					if(filterContext.Controller.ValueProvider.GetValue(kvp.Key) != null)
					{
						lookFor = kvp.Value;
						param = kvp.Key;
						break;
					}
				}
			}

			var rawValue = filterContext.Controller.ValueProvider.GetValue(param).AttemptedValue;
			object id = lookFor == LookFor.TestInfo ? Guid.Parse(rawValue) : (object)Convert.ToInt32(rawValue);
			Contest contest;
			switch(lookFor)
			{
				case LookFor.TestInfo:
					contest = new TestInfoById {Id = (Guid) id}.Load(Session).Problem.Contest;
					break;
				case LookFor.Contest:
					contest = Session.Get<Contest>(id);
					break;
				case LookFor.Problem:
					contest = Session.Get<Problem>(id).Contest;
					break;
				case LookFor.Test:
					contest = Session.Get<Test>(id).Problem.Contest;
					break;
				default:
					throw new InvalidOperationException("Другие LookFor не подходят");
			}


			if (!AuthorizationService.IsAllowed(UserSession.CurrentUser, contest, op.ToOperation()))
			{
				var expl = AuthorizationService.GetAuthorizationInformation(UserSession.CurrentUser, contest, op.ToOperation()).ToString();
				filterContext.Result = new UnauthorizedViewResult(op, expl);
			}
		}

		public ContestOperationCheckAttribute(ContestOperation op, LookFor lookFor = LookFor.SmartAss, string param = "id")
		{
			this.op = op;
			this.lookFor = lookFor;
			this.param = param;
		}

		public IAuthorizationService AuthorizationService { get; set; }
		public IUserSession UserSession { get; set; }
		public ISession Session { get; set; }

		readonly ContestOperation op;
		LookFor lookFor;
		string param;

		static readonly Dictionary<string, LookFor> smartAssLookUp = new Dictionary<string, LookFor>
		                                            	{
		                                            		{"ContestId", LookFor.Contest},
		                                            		{"ProblemId", LookFor.Problem}
		                                            	};
	}
}