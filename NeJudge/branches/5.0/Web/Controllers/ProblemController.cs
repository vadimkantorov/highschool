using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Broker.Common.Messages.Broker;
using Model.Factories;
using MvcContrib;
using Model;
using NHibernate;
using Rhino.ServiceBus;
using Web.DocumentFormatters;
using Web.Extensions;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
	public class ProblemController : Controller
	{
		[HttpPost, ContestOperationCheck(ContestOperation.EditProblems, LookFor.Problem)]
		public ActionResult PackTestInfo(int id)
		{
			var testInfoId = session.Get<Problem>(id).TestInfo.Id;
			bus.Send(new PackTestInfo {TestInfoId = testInfoId});
			return this.RedirectToAction(x => x.PackedTestInfo(testInfoId));
		}

		[HttpGet, ContestOperationCheck(ContestOperation.EditProblems, LookFor.TestInfo)]
		public ActionResult PackedTestInfo(Guid id)
		{
			var link = testInfoArchiveLinkCache.Get(id);
			if (link == null)
				return Content("Архивация еще не завершена. Обновите через десять секунд, пожалуйста.");

			return View((object)link);
		}

		string RenderDocumentBody(FormattedDocument doc)
		{
			var formatter = formatters.Find(doc.BodyFormatterId);
			return formatter.RenderHtml(doc.Body);
		}
		
		[HttpGet, ContestOperationCheck(ContestOperation.View, LookFor.Problem)]
		public ActionResult View(int id)
		{
			var problem = session.Get<Problem>(id);
			return View(new ViewProblemForm
			            	{
			            		Limits = problem.Limits,
								Name = problem.Statement.Name,
								RenderedBody = RenderDocumentBody(problem.Statement)
			            	});
		}

		[HttpGet, ContestOperationCheck(ContestOperation.EditProblems, LookFor.Problem)]
		public ActionResult Edit(int id)
		{
			var problem = session.Get<Problem>(id);
			var langs = session.CreateCriteria<Language>().List<Language>();
			var checker = problem.TestInfo.Checker;
			return View(new EditProblemForm
			            	{
			            		ProblemId = problem.Id,
			            		CheckerArguments = problem.TestInfo.CheckerArguments,
			            		CheckerSource = checker.Code,
			            		CheckerLanguages = Choose.Create(langs, l => l.Name, l => l.Name, checker.LanguageId),
			            		DocumentFormatters =
			            			Choose.Create(formatters.GetAll(), f => f.Name, f => f.Name, problem.Statement.BodyFormatterId),
			            		Limits = problem.Limits,
			            		ShortName = problem.ShortName,
			            		Name = problem.Statement.Name,
								ProblemBody = problem.Statement.Body,
								Tests = problem.TestInfo.Tests
			            	});
		}

		[HttpPost, ContestOperationCheck(ContestOperation.EditProblems), ValidateInput(false)]
		public ActionResult Update(EditProblemForm model)
		{
			var problem = session.Get<Problem>(model.ProblemId);
			problem.ShortName = model.ShortName;
			problem.Statement.Name = model.Name;
			problem.Statement.Body = model.ProblemBody;
			problem.Statement.BodyFormatterId = model.DocumentFormatters.SelectedValue;
			problem.TestInfo.Checker.LanguageId = model.CheckerLanguages.SelectedValue;
			problem.TestInfo.Checker.Code = model.CheckerSource;
			problem.TestInfo.CheckerArguments = model.CheckerArguments;
			problem.Limits = model.Limits;
			session.Save(problem);
			
			var newTests = model.NewTestsArchive;
			if (newTests != null)
			{
				var testsZipArchive = new BinaryReader(newTests.InputStream).ReadBytes(newTests.ContentLength);
				bus.Send(new UnpackTestInfo {ProblemId = problem.Id, ZipArchive = testsZipArchive, InsertAt = model.InsertNewTestsAt});
			}

			return this.RedirectToAction<ContestController>(x => x.Edit(problem.Contest.Id));
		}

		[HttpPost, ContestOperationCheck(ContestOperation.ManageProblems)]
		public ActionResult Create(NewProblemForm model)
		{
			var problem = new Problem
			{
				Contest = session.Load<Contest>(model.ContestId),
				ShortName = model.ShortName,
				Statement = new FormattedDocument {Name = model.Name},
			};
			session.Save(problem);
			return this.RedirectToAction<ContestController>(x => x.Edit(problem.Contest.Id));
		}

		[HttpGet, ContestOperationCheck(ContestOperation.ManageProblems)]
		public ActionResult New(int id)
		{
			return View(new NewProblemForm
			            	{
			            		ContestId = id,
			            		ShortName = SuggestShortName(id),
			            	});
		}

		string SuggestShortName(int contestId)
		{
			Func<string, bool> goodProblemShortName = x => x.Length == 1 && char.IsLetter(x[0]) && char.IsUpper(x[0]);
			
			var problemShortNames = session.Get<Contest>(contestId).Problems.Select(x => x.ShortName).ToArray();
			if (problemShortNames.Length == 0)
				return "A";
			if(problemShortNames.Any(x => !goodProblemShortName(x) || problemShortNames.Length == 26))
				throw new InvalidOperationException("Поддерживаются только задачи с однобуквенными короткими именами (и не больше 26 штук)");
			
			return ((char)(problemShortNames.Max()[0] + 1)).ToString();
		}

		public ProblemController(IServiceBus bus, ISession session, ITestInfoArchiveLinkCache testInfoArchiveLinkCache, IFactory<IFormatter> formatters)
		{
			this.bus = bus;
			this.session = session;
			this.testInfoArchiveLinkCache = testInfoArchiveLinkCache;
			this.formatters = formatters;
		}

		readonly IServiceBus bus;
		readonly ISession session;
		readonly ITestInfoArchiveLinkCache testInfoArchiveLinkCache;
		readonly IFactory<IFormatter> formatters;
	}
}