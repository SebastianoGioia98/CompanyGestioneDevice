using Microsoft.AspNetCore.Mvc;

namespace Company.GestioneDevice.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
