using System.Web.Mvc;
using MvcContrib;
using MvcContrib.Attributes;
using Web.ViewModels;
using Web.Services;

namespace Web.Controllers
{
	public class LoginController : Controller
	{
		public ActionResult Snippet()
		{
			return View(userSession.IsAuthenticated ? userSession.CurrentUser : null);
		}
		
		[HttpGet]
		public ActionResult Index()
		{
			return View(new LoginForm());
		}

		[HttpPost]
		public ActionResult LogIn(LoginForm form, string returnUrl)
		{
			var user = authn.Authenticate(form.UserName, form.Password);
			if (user == null)
				return View("Index", form.WithErrorMessage("Неправильная пара логин-пароль"));

			userSession.LogIn(user);
			if (!string.IsNullOrEmpty(returnUrl))
				return Redirect(returnUrl);
			else
				return this.RedirectToAction<HomeController>(x => x.Index());
		}

		[HttpPost]
		public ActionResult LogOut()
		{
			userSession.LogOut();
			return this.RedirectToAction<HomeController>(x => x.Index());
		}

		public LoginController(IAuthenticationService authn, IUserSession userSession)
		{
			this.authn = authn;
			this.userSession = userSession;
		}

		readonly IAuthenticationService authn;
		readonly IUserSession userSession;
	}
}