using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace jaytwo.MiniRouter.example.AspNetCore3_0.Controllers
{
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            var message = $"[{GetType().Assembly.GetName().Name}]\n";
            message += $"Hello World!";

            return Content(message);
        }
    }
}
