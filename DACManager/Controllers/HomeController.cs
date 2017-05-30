using DACManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DACManager.Controllers
{
	public class HomeController : Controller
	{
		private readonly SignInManager<ApplicationUser> _signInManager;

		public HomeController(SignInManager<ApplicationUser> signInManager)
		{
			_signInManager = signInManager;
		}

		public IActionResult Index()
		{
			if (_signInManager.IsSignedIn(User))
				return View();

			return Unauthorized();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Brief description";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Contact page";

			return View();
		}

		public IActionResult AccessDenied()
		{
			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}
