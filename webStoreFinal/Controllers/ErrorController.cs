using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webStoreFinal.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult ShowError()
        {
            StatusCode(404);
            return View();
        }
    }
}