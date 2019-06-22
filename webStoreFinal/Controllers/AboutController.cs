using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webStoreFinal.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.pageName = "About Us";
            return View();
        }
    }
}