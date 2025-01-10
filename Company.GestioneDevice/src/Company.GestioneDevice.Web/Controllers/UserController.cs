using Microsoft.AspNetCore.Mvc;
using System;

namespace Company.GestioneDevice.Web.Controllers;

public class UserController : Controller
{
    [Route("users")]
    public IActionResult Index()
    {
        return View();
    }



    //[Route("user/{userId}")]
    //public IActionResult Content(Guid deviceId)
    //{
    //    ViewBag.DeviceId = deviceId;
    //    return View();
    //}
}
