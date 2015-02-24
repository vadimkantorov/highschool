using System;
using DataAccess.Queries.Users;
using MvcContrib;
using System.IO;
using System.Threading;
using System.Web.Mvc;
using Broker.Common.Messages.Broker;
using Model;
using Model.Testing;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Rhino.Security.Interfaces;
using Rhino.ServiceBus;
using Web.Services;
using Environment=System.Environment;
using IAuthorizationService = Rhino.Security.Interfaces.IAuthorizationService;

namespace Web.Controllers
{
	public class DeploymentController : Controller
	{
		public void Test()
		{
			var user = new UserByUserName {UserName = "admin"}.Load(session);
			var contest = session.CreateCriteria<Contest>().List<Contest>()[0];

			rep.CreateOperation("/Xyu/Pizda");
			var everyone = rep.CreateUsersGroup("Everyone");
			rep.AssociateUserWith(user, everyone);

			pbs.Allow("/Xyu/Pizda").For(everyone).OnEverything().DefaultLevel().Save();

			var xxx = r.IsAllowed(user, contest, "/Xyu/Pizda");
			xxx = xxx;
			return;
		}

		public ActionResult Deploy()
		{
			new SchemaExport(cfg).Execute(false, true, false);
			var realProblemDirectory = @"H:\Projects\NeJudge\SRC\Tests\Tester\TestPrograms\RealProblem";

			var zip = System.IO.File.ReadAllBytes(Path.Combine(realProblemDirectory, "tests2.zip"));
			var user = new User { DisplayName = "Игрок", UserName = "gamer", Password = new Password("")};
			var judge = new User {DisplayName = "Судья", UserName = "judge", Password = new Password("")};

			var contest = new Contest
			              	{
			              		Beginning = new DateTime(1990, 7, 7),
			              		Ending = new DateTime(2990, 7, 7),
			              		Type = "Icpc",
			              		Announcement = new FormattedDocument {Name = "Тестовый контест"},
								Owner = judge,
								Judge = judge
			              	};
			var problem = new Problem
			{
				Contest = contest,
				Limits = new ResourceUsage
				{
					MemoryInBytes = int.MaxValue,
					TimeInMilliseconds = 100500
				},
				TestInfo = new TestInfo
				{
					Checker = new ProgramSource
					{
						LanguageId = "MSVC90Testlib",
						Code = System.IO.File.ReadAllText(Path.Combine(realProblemDirectory, "check.cpp"))
					},
				},
				ShortName = "A",
				Statement = new FormattedDocument
				{
					Name = "Задача A"
				}

			};

			var answer = new Answer
			             	{
			             		Author = judge,
			             		Contest = contest,
			             		SentAt = new DateTime(2009, 1, 3),
			             		Subject = "Re: Вопрос по задаче A",
			             		Text = "А хрен вам!",
								Recipient = user
			             	};

			var question = new Question
			{
				Author = user,
				Contest = contest,
				SentAt = new DateTime(2009, 1, 2),
				Subject = "Вопрос по задаче A",
				Text = "Где взять условие?",
				Next = answer
			};

			var announcement = new Announcement
			                   	{
			                   		Author = judge,
			                   		Contest = contest,
			                   		SentAt = new DateTime(2009, 1, 1),
			                   		Subject = "Просмотрите тестовый контест",
			                   		Text = "Объявление о тестовом контесте"
			                   	};

			session.Delete("from Problem p");
			session.Delete("from Contest p");
			session.Delete("from User p");
			session.Delete("from Language p");
			session.Delete("from Message p");
			session.Delete("from Test t");


			session.Save(announcement);
            session.Save(question);
			session.Save(answer);
			session.Save(user);
			session.Save(judge);
			session.Save(contest);
			session.Save(problem);
			session.Save(new Language { Name = "Microsoft Visual C++ 9.0" });
		
			session.Flush();
			bus.Send(new UnpackTestInfo { ZipArchive = zip, ProblemId = problem.Id });
			Thread.Sleep(5000);

			return this.RedirectToAction<HomeController>(x => x.Index());
		}

		public DeploymentController(ISession session, IServiceBus bus, Configuration cfg, IPermissionsBuilderService pbs, IAuthorizationService r, IAuthorizationRepository rep)
		{
			this.session = session;
			this.bus = bus;
			this.cfg = cfg;
			this.pbs = pbs;
			this.r = r;
			this.rep = rep;
		}

		readonly ISession session;
		readonly IServiceBus bus;
		readonly Configuration cfg;
		readonly IPermissionsBuilderService pbs;
		readonly IAuthorizationService r;
		readonly IAuthorizationRepository rep;
	}
}