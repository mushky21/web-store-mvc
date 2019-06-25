using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webStoreFinal.ViewComponents.LoginViewComponent
{
    public class Login : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string greeting;
            if (DateTime.Now.Hour > 6 && DateTime.Now.Hour < 12)
            {
                greeting = "Good Morning";
            }
            else if (DateTime.Now.Hour > 12 && DateTime.Now.Hour < 18)
            {
                greeting = "good Afternoon";
            }
            else
            {
                greeting = "Good Evening";
            }

            ViewBag.greeting = greeting;
            return await Task.FromResult<IViewComponentResult>(View("Greeting"));
        }
    }
}

