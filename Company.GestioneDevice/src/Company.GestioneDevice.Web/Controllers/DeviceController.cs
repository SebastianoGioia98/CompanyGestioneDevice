using Microsoft.AspNetCore.Mvc;
using System;

namespace Company.GestioneDevice.Web.Controllers;

public class DeviceController : Controller
{
    [Route("devices")]
    public IActionResult Index()
    {
        return View();
    }



    [Route("devices/{deviceId}")]
    public IActionResult Content(Guid deviceId)
    {
        ViewBag.DeviceId = deviceId;
        return View();
    }
}
