using Microsoft.AspNetCore.Mvc;
using MS147Site.Models;

namespace MS147Site.Controllers
{
    public class ApiController : Controller
    {
        private readonly DemoContext _context;
        private readonly IWebHostEnvironment _host;

        public ApiController(DemoContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }
        public IActionResult Index()
        {
            //return Content("Hi");
            //return Content("<H1>Hi</H1>","text/html");
            //return Content("<H1>你好挖</H1>", "text/html",System.Text.Encoding.UTF8);
            return Content("Hello Ajax");
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

        public IActionResult GetDistricts(string city)
        {
            {
                var district = _context.Address.Where(x=>x.SiteId.Contains(city)).Select(x => x.SiteId.Substring(3,6)).Distinct();
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

        public IActionResult AjaxEvent(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "Guest";
            }
            System.Threading.Thread.Sleep(3000);
            return Content("Hello "+name);
        }

        public IActionResult Register(Members member,IFormFile file)
        {
            string path = Path.Combine(_host.WebRootPath, "uploads", file.FileName);
            using (var filestream=new FileStream(path, FileMode.Create)){
                file.CopyTo(filestream);
            }

            member.FileName = file.FileName;
            byte[]? st = null;
            using (var memory=new MemoryStream())
            {
                file.CopyTo(memory);
                st = memory.ToArray();
            }

            member.FileData = st;

            _context.Members.Add(member);
            _context.SaveChanges();

                return Content(path);
        }

        public IActionResult GetImageByte(int id = 0)
        {
            Members? members = _context.Members.Find(id);
            byte[]? img = members.FileData;

            return File(img, "image/jpeg");
        }

        public IActionResult GetImg(IFormFile file)
        {
            byte[]? st = null;
            using (var memory = new MemoryStream())
            {
                file.CopyTo(memory);
                st = memory.ToArray();
            }

            return File(st, "image/jpeg");
        }

        public IActionResult GetName()
        {
            var getName = _context.Members.Select(x=>x.Name);

            return Json(getName);
        }
    }
}
