#if NET45
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using jaytwo.MiniRouter;
using jaytwo.MiniRouter.AspNet;

namespace jaytwo.MiniRouter.example.AspNet
{
    public class HelloMiniRouteHandler : MiniRouterHandler
    {
        public HelloMiniRouteHandler()
            : base(new HelloMiniRouter())
        {
        }
    }
}
#endif
