using Credibill_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Credibill_Web.Controllers
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

        public IActionResult Customer()
        {
            return RedirectToAction("Index", "Customers");
        }

        public IActionResult Invoice()
        {
            return RedirectToAction("Index", "Invoices");
        }

        public IActionResult Payment()
        {
            return RedirectToAction("Index", "Payments");
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
    }
}
