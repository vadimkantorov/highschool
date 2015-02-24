using System;
using System.Collections.Generic;
using System.Linq;
using Broker;
using Model.Factories;
using MvcContrib;
using System.Web.Mvc;
using DataAccess.Queries.Submissions;
using Model;
using NHibernate;
using NHibernate.Criterion;
using Rhino.Security.Interfaces;
using Web.Extensions;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
	public class ContestController : Controller
	{
		[HttpGet]
		public ActionResult Users(int id)
		{
			var contest = session.Get<Contest>(id);
			
			IEnumerable<User> users;
			if (contest.IsPublic)
				users = new SubmissionsSortedByTimeDesc { ContestId = id }.Load(session).List<Submission>().Select(x => x.Author);
			else
				users = contest.Participants.Where(x => x.IsApproved).Select(x => x.User);

			return View(users);
		}
		
		[HttpGet, ContestOperationCheck(ContestOperation.ChangeAdministration)]
		public ActionResult ChangeAdministration(int id)
		{
			var contest = session.Get<Contest>(id);
			var administrationCandidates = session.CreateCriteria<User>().List<User>();

			return PartialView(new ChangeContestAdministrationForm
			               	{
			               		ContestId = contest.Id,
			               		Owners = Choose.Create(administrationCandidates, x => x.DisplayName, x => x.Id, contest.Owner.Id),
			               		Judges = Choose.Create(administrationCandidates, x => x.DisplayName, x => x.Id, contest.Judge.Id)
			               	});
		}

		[HttpPost, ContestOperationCheck(ContestOperation.ChangeAdministration)]
		public ActionResult ChangeAdministration(ChangeContestAdministrationForm form)
		{
			
			var contest = session.Load<Contest>(form.ContestId);
			var oldOwner = contest.Owner;
			var oldJudge = contest.Judge;
			
			contest.Owner = session.Load<User>(Convert.ToInt32(form.Owners.SelectedValue));
			contest.Judge = session.Load<User>(Convert.ToInt32(form.Judges.SelectedValue));
			session.Save(contest);

			interceptor.OnJudgeChanged(contest, oldJudge);
			interceptor.OnOwnerChanged(contest, oldOwner);

			return this.RedirectToAction(x => x.Edit(form.ContestId));
		}

		[HttpGet, ContestOperationCheck(ContestOperation.ViewMonitor)]
		public ActionResult Monitor(int id)
		{
			var contest = session.Get<Contest>(id);
			var handler = contestTypeHandlerFactory.Find(contest.Type);
			var monitor = monitorCache.Get(id);

			if(monitor == null)
				return Content("Монитор ещё недоступен. Обновите через десять секунд, пожалуйста.");

			return Content(handler.RenderMontior(monitor));
		}

		[HttpGet]
		public ActionResult Status(int id)
		{
			var subms = new SubmissionsSortedByTimeDesc {ContestId = id}.Load(session);
			subms = authorizationInfo.Add(subms, SubmissionOperation.View);
			
			return View(new ContestStatusForm
				{
					Submissions = subms.List<Submission>()
				});
		}

		[HttpGet, ContestOperationCheck(ContestOperation.View)]
		public ActionResult View(int id)
		{
			return View(session.Get<Contest>(id));
		}

		[HttpGet]
		public ActionResult Index()
		{
			var crit = authorizationInfo.Add(session.CreateCriteria<Contest>(), ContestOperation.View);
			return View(crit.List<Contest>());
		}

		[HttpGet, SystemOperationCheck(SystemOperation.ManageContests)]
		public ActionResult New()
		{
			return View(new NewContestForm
				{
					ContestTypes = ContestTypes(null),
					Beginning = DateTime.Today.AddMonths(1).AddHours(13),
					Ending = DateTime.Today.AddMonths(1).AddHours(18)
				});
		}

		[HttpPost, SystemOperationCheck(SystemOperation.ManageContests)]
		public ActionResult Create(NewContestForm model)
		{
			var contest = new Contest
				{
					Beginning = model.Beginning,
					Ending = model.Ending,
					Owner = userSession.CurrentUser,
					Judge = userSession.CurrentUser,
					Type = model.ContestTypes.SelectedValue,
					Announcement = new FormattedDocument { Name = model.Name},
				};
			session.Save(contest);
			interceptor.OnCreated(contest);

			return this.RedirectToAction(x => x.Edit(contest.Id));
		}

		[HttpPost, ContestOperationCheck(ContestOperation.EditSettings)]
		public ActionResult Update(EditContestForm model)
		{
			var contest = session.Get<Contest>(model.ContestId);
			var oldIsPublic = contest.IsPublic;

			contest.Beginning = model.Beginning;
			contest.Ending = model.Ending;
			contest.Type = model.ContestTypes.SelectedValue;
			contest.Announcement.Name = model.Name;
			contest.IsPublic = model.IsPublic;
			session.Save(contest);

			interceptor.OnIsPublicChanged(contest, oldIsPublic);

			return this.RedirectToAction(x => x.Edit(contest.Id));
		}

		[HttpGet, ContestOperationCheck(ContestOperation.EditSettings)]
		public ActionResult Edit(int id)
		{
			var contest = session.Get<Contest>(id);
			return View(new EditContestForm
			            	{
			            		ContestId = contest.Id,
			            		Beginning = contest.Beginning,
			            		Ending = contest.Ending,
			            		ContestTypes = ContestTypes(contest.Type),
			            		Name = contest.Announcement.Name,
			            		Problems = contest.Problems,
			            		IsPublic = contest.IsPublic
			            	});
		}

		Choose ContestTypes(string contestType)
		{
			return Choose.Create(contestTypeHandlerFactory.GetAll(), x => x.Name, x => x.Name, contestType);
		}

		public ContestController(
			AuthorizationHelper authorizationInfo,
			ISession session,
			IFactory<IContestTypeHandler> contestTypeHandlerFactory,
			IMonitorCache monitorCache, 
			IUserSession userSession,
			NhContestSecurityInterceptor interceptor)
		{
			this.authorizationInfo = authorizationInfo;
			this.session = session;
			this.userSession = userSession;
			this.interceptor = interceptor;
			this.contestTypeHandlerFactory = contestTypeHandlerFactory;
			this.monitorCache = monitorCache;
		}

		private readonly AuthorizationHelper authorizationInfo;
		readonly ISession session;
		readonly IFactory<IContestTypeHandler> contestTypeHandlerFactory;
		readonly IMonitorCache monitorCache;
		readonly IUserSession userSession;
		readonly NhContestSecurityInterceptor interceptor;
	}
}