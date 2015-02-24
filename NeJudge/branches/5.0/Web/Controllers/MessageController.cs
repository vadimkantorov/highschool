using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataAccess.Queries.Messages;
using Model;
using NHibernate;
using Web.Extensions;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
	public partial class MessageController : Controller
	{
		[HttpGet, MessageOperationCheck(MessageOperation.View)]
		public ActionResult View(int id)
		{
			var msg = session.Get<Message>(id);
			session.Save(new MessageReading {Message = msg, User = userSession.CurrentUser, IsRead = true});
			return View(msg);
		}
		
		[HttpGet]
		public ActionResult Index(int id)
		{
			var curUser = userSession.CurrentUser;
			var contest = session.Get<Contest>(id);

			IEnumerable<MessageReading> msgs;
			if (true)
				msgs = new MessagesForJudge {Contest = contest, Judge = curUser}.List(session);
			else
				msgs = new MessagesForContestant { Contest = contest, Contestant = curUser }.List(session);

			return View(new MessagesForm
			{
				Messages = msgs.OrderByDescending(x => x.Message.SentAt),
				Contest = contest
			});
		}

		ActionResult NewMessage(int id)
		{
			var contests = session.CreateCriteria<Contest>().List<Contest>();
			return View(Choose.Create(contests, x => x.Announcement.Name, x => x.Id, id));
		}

		ActionResult MessageSentSuccessfully()
		{
			return View();
		}

		public MessageController(IUserSession userSession, ISession session, IClock clock)
		{
			this.userSession = userSession;
			this.clock = clock;
			this.session = session;
		}

		readonly IUserSession userSession;
		readonly ISession session;
		readonly IClock clock;
	}
}