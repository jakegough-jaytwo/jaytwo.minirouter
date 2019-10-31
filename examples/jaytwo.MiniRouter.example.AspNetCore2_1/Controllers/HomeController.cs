using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace jaytwo.MiniRouter.example.AspNetCore2_1.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            var message = $"[{GetType().Assembly.GetName().Name}]\n";
            message += $"Hello\n";
            message += $"World\n";

            return Content(message);
        }
    }
}
