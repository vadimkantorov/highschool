using System.Web.Mvc;
using Model;
using Web.Services;

namespace Web.Controllers
{
	public partial class MessageController
	{
		[HttpPost]
		public ActionResult CreateQuestion(int id, string text)
		{
			var curUser = userSession.CurrentUser;
			var curTime = clock.CurrentTime;

			var q = new Question
			{
				SentAt = curTime,
				Author = curUser,
				Contest = session.Get<Contest>(id),
				Text = text,
				Subject = string.Format("Вопрос от {0} ({1})", curUser.DisplayName, curTime)
			};

			session.Save(q);
			return MessageSentSuccessfully();
		}

		[HttpGet]
		public ActionResult NewQuestion(int id)
		{
			return NewMessage(id);
		}
	}
}