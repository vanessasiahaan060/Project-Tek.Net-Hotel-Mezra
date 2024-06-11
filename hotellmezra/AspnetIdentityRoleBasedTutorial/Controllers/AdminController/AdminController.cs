using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspnetIdentityRoleBasedTutorial.Controllers.AdminController
{
    [Authorize(Roles = "Admin")]

    public class AdminController : Controller
	{
		public IActionResult Beranda()
		{
			return View();
		}
	}
}
