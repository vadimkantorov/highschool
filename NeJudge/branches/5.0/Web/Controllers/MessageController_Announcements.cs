using System.Web.Mvc;
using Model;
using Web.Services;

namespace Web.Controllers
{
	public partial class MessageController
	{
		[HttpPost, ContestOperationCheck(ContestOperation.ManageMessages)]
		public ActionResult CreateAnnouncement(int id, string subject, string text)
		{
			var contest = session.Get<Contest>(id);
			var announcement = new Announcement
			{
				SentAt = clock.CurrentTime,
				Author = userSession.CurrentUser,
				Contest = contest,
				Text = text,
				Subject = subject
			};

			session.Save(announcement);
			return MessageSentSuccessfully();
		}


		[HttpGet, ContestOperationCheck(ContestOperation.ManageMessages)]
		public ActionResult NewAnnouncement(int id)
		{
			return NewMessage(id);
		}
	}
}