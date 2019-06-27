using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webStoreFinal.Services
{


    public class GreetingService:IGreetingService
    {
        public string GreetingContent()
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
            return greeting;
        }
    }

}
