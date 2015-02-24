using System;
using System.Linq;
using System.Web.Mvc;
using DataAccess.Queries.ParticipationApplication;
using Model;
using MvcContrib;
using NHibernate;
using Web.Security;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
	public class ParticipationApplicationController : Controller
	{
		[HttpGet]
		public ActionResult New(int id)
		{
			return View(id);
		}

		[HttpPost]
		public ActionResult Create(int id)
		{
			var application = new ParticipationApplication
			                  	{
			                  		Contest = session.Load<Contest>(id),
			                  		User = userSession.CurrentUser,
			                  		SubmittedAt = clock.CurrentTime,
			                  		IsApproved = false
			                  	};
			session.Save(application);
			return this.RedirectToAction<ContestController>(x => x.View(id));
		}
		
		[HttpGet, ContestOperationCheck(ContestOperation.ManageParticipationApplications)]
		public ActionResult Index(int id)
		{
			var contest = session.Get<Contest>(id);
			var applications = contest.Participants.OrderBy(x => x.SubmittedAt);

			var form = new EditParticipationApplicationsForm
			           	{
			           		Applications =
			           			applications.Select(x => new EditParticipationApplicationForm {Id = x.Id, IsApproved = x.IsApproved, SubmittedAt = x.SubmittedAt, UserDisplayName = x.User.DisplayName}).ToList(),
			           		ContestId = contest.Id,
							ContestName = contest.Announcement.Name,
			           	};
			return View(form);
		}

		[HttpPost, ContestOperationCheck(ContestOperation.ManageParticipationApplications)]
		public ActionResult Update(EditParticipationApplicationsForm form)
		{
			foreach(var applicationForm in form.Applications)
			{
				var application = session.Get<ParticipationApplication>(applicationForm.Id);
				var oldIsApproved = application.IsApproved;
				application.IsApproved = applicationForm.IsApproved;
				session.Save(application);
				interceptor.OnIsApprovedChanged(application, oldIsApproved);
			}
			return this.RedirectToAction<ContestController>(x => x.Edit(form.ContestId));
		}

		public ParticipationApplicationController(ISession session, IUserSession userSession, IClock clock, NhParticipationApplicationSecurityInterceptor interceptor)
		{
			this.session = session;
			this.userSession = userSession;
			this.clock = clock;
			this.interceptor = interceptor;
		}

		readonly ISession session;
		readonly IUserSession userSession;
		readonly IClock clock;
		readonly NhParticipationApplicationSecurityInterceptor interceptor;
	}
}