using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UserSideWEB.Models;

namespace UserSideWEB.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataLayer.DataLayer _dataLayer;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _dataLayer = new DataLayer.DataLayer();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Form(CreateApplicationForm form)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("Confirm");
            }
            return View();
        }
        public IActionResult Confirm()
        {
            return View();
        }
        public IActionResult Form()
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
