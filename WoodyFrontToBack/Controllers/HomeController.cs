using Microsoft.AspNetCore.Mvc;

namespace WoodyFrontToBack.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
