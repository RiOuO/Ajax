using Microsoft.AspNetCore.Mvc;
using MS147Site.Models;

namespace MS147Site.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _context;

        public ApiController(DemoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //return Content("Hi");
            //return Content("<H1>Hi</H1>","text/html");
            return Content("<H1>你好挖</H1>", "text/html",System.Text.Encoding.UTF8);
        }

        public IActionResult CheckAccount(string name)
        {
            {
                var nameInDb=_context.Members.FirstOrDefault(x => x.Name == name);
                return Json(nameInDb);
            }
        }

        public IActionResult GetCity()
        {
            {
                var city = _context.Address.Select(x => x.City).Distinct();
                return Json(city);
            }
        }

        public IActionResult GetDistricts(string districts)
        {
            {
                var district = _context.Address.Where(x=>x.SiteId.Contains(districts)).Select(x => x.SiteId.Substring(3,6)).Distinct();
                return Json(district);
            }
        }

        public IActionResult GetRoad(string districts)
        {
            {
                var road = _context.Address.Where(x => x.SiteId.Contains(districts)).Select(y => y.Road);
                return Json(road);
            }
        }
    }
}
