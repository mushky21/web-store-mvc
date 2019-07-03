using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webStoreFinal.Models;

namespace webStoreFinal.ViewComponents
{
    public class ErrorHandleViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync()
        {
        
            ViewBag.pageName = "404 not found error";
            return Task.FromResult<IViewComponentResult>(View("ProductNotFound"));


        }
    }
}

