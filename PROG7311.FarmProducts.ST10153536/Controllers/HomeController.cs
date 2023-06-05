using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 
using PROG7311.FarmProducts.ST10153536.Models;
using System.Diagnostics;

namespace PROG7311.FarmProducts.ST10153536.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AdminDashboard()
        {
            return View("AdminDashboard");
        }

        public IActionResult EmployeeDashboard()
        {
            return View("EmployeeDashboard");
        }

        public IActionResult FarmerDashboard()
        {
            return View("FarmerDashboard");
        }
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View("Login");
        }
    }
}
