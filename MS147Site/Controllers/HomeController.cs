using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MS147Site.Models;
using System.Diagnostics;

namespace MS147Site.Controllers
{
    public class HomeController : Controller
    {
        private DemoContext _context;

        public HomeController(DemoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cities = _context.Address.Select(x => x.City).Distinct();
            return Json(cities);
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