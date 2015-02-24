using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Model;
using Model.Utils;
using NHibernate;
using Web.Extensions;
using Web.Services;

namespace Web.Controllers
{
	public class UserController : Controller
	{
		[HttpGet, SystemOperationCheck(SystemOperation.ManageUsers)]
		public ActionResult Index()
		{
			return View(session.CreateCriteria<User>().List<User>());
		}

		[HttpPost, SystemOperationCheck(SystemOperation.ManageUsers)]
		public ActionResult MassCreate(string familyNames, int? contestId)
		{
			var lines = familyNames.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
			var contest = contestId == null ? null : session.Load<Contest>(contestId);
			
			var res = new List<Tuple<string, User>>();
			foreach(var line in lines)
			{
				var password = StringUtils.GenerateRandomLatinString(GeneratedUserNameAndPasswordLength);
				var userName = StringUtils.GenerateRandomLatinString(GeneratedUserNameAndPasswordLength);

				var user = new User {DisplayName = line, UserName = userName, Password = new Password(password)};
				session.Save(user);
				interceptor.OnCreated(user);

				
				if(contest != null)
				{
					var application = new ParticipationApplication
					                  	{
					                  		Contest = contest,
					                  		IsApproved = true,
					                  		SubmittedAt = clock.CurrentTime,
					                  		User = user
					                  	};
					session.Save(application);
				}

				res.Add(Tuple.Create(password, user));
			}
			return View(res);
		}

		[HttpGet, SystemOperationCheck(SystemOperation.ManageUsers)]
		public ActionResult MassNew()
		{
			var contests = session.CreateCriteria<Contest>().List<Contest>();
			return View(Choose.Create(contests, x => x.Announcement.Name, x => x.Id));
		}
		
		[HttpGet]
		public ActionResult New()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(string displayName, string userName, string password)
		{
			var user = new User {DisplayName = displayName, UserName = userName, Password = new Password(password)};
			session.Save(user);
			interceptor.OnCreated(user);
			return View(user);
		}

		public UserController(ISession session, IClock clock, NhUserSecurityInterceptor interceptor)
		{
			this.session = session;
			this.clock = clock;
			this.interceptor = interceptor;
		}

		readonly ISession session;
		readonly IClock clock;
		readonly NhUserSecurityInterceptor interceptor;
		const int GeneratedUserNameAndPasswordLength = 8;
	}
}