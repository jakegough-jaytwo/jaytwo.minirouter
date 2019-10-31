using System;
using System.Web.Mvc;

namespace jaytwo.MiniRouter.example.AspNet4_6_1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("~/api/Values");
        }
    }
}
