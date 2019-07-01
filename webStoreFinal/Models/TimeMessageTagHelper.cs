using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webStoreFinal.Models
{
    public static class TimeMessageTagHelper
    {
        public static string TimeMessage(this IHtmlHelper htmlHelper)
        {
            int hour = DateTime.Now.Hour;
            string greeting;
            if (hour > 6 && hour < 12)
            {
                greeting = "Good Morning";
            }
            else if (hour > 12 && hour < 18)
            {
                greeting = "good Afternoon";
            }
            else
            {
                greeting = "Good Evening";
            }
            return greeting;
        }
    }
}
