using System.Web.Mvc;
using Model;
using Web.Services;

namespace Web.Controllers
{
	public partial class MessageController
	{
		[HttpPost, ContestOperationCheck(ContestOperation.ManageMessages)]
		public ActionResult CreateAnswer(int id, string answer)
		{
			var message = session.Get<Message>(id);
			var clarification = new Answer
			{
				SentAt = clock.CurrentTime,
				Author = userSession.CurrentUser,
				Contest = message.Contest,
				Text = answer,
				Subject = "Re: " + message.Subject
			};

			message.Next = clarification;

			session.Save(clarification);
			session.Save(message);
			return MessageSentSuccessfully();
		}

		[HttpGet, ContestOperationCheck(ContestOperation.ManageMessages)]
		public ActionResult NewAnswer(int id)
		{
			return View(session.Get<Message>(id));
		}
	}
}