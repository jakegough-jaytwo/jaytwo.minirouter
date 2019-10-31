#if NET45
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jaytwo.MiniRouter.AspNet;

namespace jaytwo.MiniRouter.example.AspNet
{
    public class HelloMiniRouteModule : MiniRouterModule
    {
        public HelloMiniRouteModule(string path = "hellomodule")
            : base(path, () => new HelloMiniRouteHandler())
        {
        }
    }
}
#endif
