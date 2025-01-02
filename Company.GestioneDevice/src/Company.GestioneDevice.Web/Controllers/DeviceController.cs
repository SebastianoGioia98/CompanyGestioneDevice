using Microsoft.AspNetCore.Mvc;

namespace Company.GestioneDevice.Web.Controllers;

public class DeviceController : Controller
{
    [Route("devices")]
    public IActionResult Index()
    {
        return View();
    }
}
