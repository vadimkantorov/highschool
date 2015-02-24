using System.Web.Mvc;
using Model;
using Web.Services;

namespace Web.Controllers
{
	public partial class MessageController
	{
		[HttpPost, ContestOperationCheck(ContestOperation.ManageMessages)]
		public ActionResult CreateClarification(int id, string subject, string text)
		{
			var contest = session.Get<Contest>(id);
			var clarification = new Clarification
			{
				SentAt = clock.CurrentTime,
				Author = userSession.CurrentUser,
				Contest = contest,
				Text = text,
				Subject = subject
			};

			session.Save(clarification);
			return MessageSentSuccessfully();
		}

		[HttpGet, ContestOperationCheck(ContestOperation.ManageMessages)]
		public ActionResult NewClarification(int id)
		{
			return NewMessage(id);
		}
	}
}