using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using jaytwo.MiniRouter.example.AspNet;

namespace jaytwo.MiniRouter.example.AspNet4_6_1
{
    public class Global : System.Web.HttpApplication
    {
        private static readonly HelloMiniRouteModule _helloMiniRouteModule = new HelloMiniRouteModule();

        public override void Init()
        {
            base.Init();
            _helloMiniRouteModule.Init(this);
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
