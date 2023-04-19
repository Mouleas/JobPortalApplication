using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Controllers
{
    public class LandingPage : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

    }
}
