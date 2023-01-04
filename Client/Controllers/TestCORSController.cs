using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class TestCORSController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
