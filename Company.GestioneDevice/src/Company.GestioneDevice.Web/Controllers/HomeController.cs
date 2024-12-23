using Microsoft.AspNetCore.Mvc;

namespace WarpDrive.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
