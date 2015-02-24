using System.Text;
using System.Web.Mvc;
using Model;
using NHibernate;
using Web.Services;

namespace Web.Controllers
{
	[ContestOperationCheck(ContestOperation.EditProblems, LookFor.Test)]
	public class TestController : Controller
	{
		[HttpGet]
		public ActionResult ViewInput(int id)
		{
			return Content(Encoding.ASCII.GetString(session.Get<Test>(id).Input.Bytes));
		}

		[HttpGet]
		public ActionResult ViewOutput(int id)
		{
			return Content(Encoding.ASCII.GetString(session.Get<Test>(id).Output.Bytes));
		}

		[HttpPost]
		public ActionResult Delete(int id)
		{
			session.Load<Test>(id).Problem.TestInfo.RemoveTest(id);

			return Content("Тест #" + id + " удалён");
		}

		public TestController(ISession session)
		{
			this.session = session;
		}

		readonly ISession session;
	}
}