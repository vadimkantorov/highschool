using System.Web.Mvc;
using Broker.Common.Messages.Broker;
using Model;
using NHibernate;
using MvcContrib;
using Rhino.ServiceBus;
using Web.Extensions;
using Web.Security;
using Web.Services;
using Web.ViewModels;
using IAuthorizationService = Rhino.Security.Interfaces.IAuthorizationService;

namespace Web.Controllers
{
	public class SubmissionController : Controller
	{
		[HttpGet, SubmissionOperationCheck(SubmissionOperation.View)]
		public ActionResult View(int id)
		{
			return View(session.Get<Submission>(id).Source);
		}

		[HttpGet, ContestOperationCheck(ContestOperation.Submit, LookFor.Problem)]
		public ActionResult New(int id)
		{
			var langs = session.CreateCriteria<Language>().List<Language>();
			return View(new NewSolutionForm
			{
				Languages = Choose.Create(langs, l => l.Name, l => l.Name),
				ProblemId = id
			});
		}

		[HttpPost, ValidateInput(false), ContestOperationCheck(ContestOperation.Submit)]
		public ActionResult Create(NewSolutionForm model)
		{
			var submission = new Submission
			{
				Source = new ProgramSource
					{
						LanguageId = model.Languages.SelectedValue,
						Code = model.Code
					},
				SubmittedAt = clock.CurrentTime,
				Problem = session.Load<Problem>(model.ProblemId),
				Author = userSession.CurrentUser
			};

			session.Save(submission);
			interceptor.OnCreated(submission);
			session.Transaction.Commit();

            bus.Send(new JudgeSubmission {SubmissionId = submission.Id});
			return this.RedirectToAction<ContestController>(x => x.Status(submission.Problem.Contest.Id));
		}

		public SubmissionController(IServiceBus bus, IUserSession userSession, IClock clock, ISession session, IAuthorizationService authz, NhSubmissionSecurityInterceptor interceptor)
		{
			this.bus = bus;
			this.interceptor = interceptor;
			this.userSession = userSession;
			this.clock = clock;
			this.session = session;
			this.authz = authz;
		}

		readonly IServiceBus bus;
		readonly IUserSession userSession;
		readonly IClock clock;
		readonly ISession session;
		readonly IAuthorizationService authz;
		readonly NhSubmissionSecurityInterceptor interceptor;
	}
}
