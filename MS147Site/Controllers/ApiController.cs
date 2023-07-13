using Microsoft.AspNetCore.Mvc;

namespace MS147Site.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index()
        {
            //return Content("Hi");
            //return Content("<H1>Hi</H1>","text/html");
            return Content("<H1>你好挖</H1>", "text/html",System.Text.Encoding.UTF8);
        }
    }
}
