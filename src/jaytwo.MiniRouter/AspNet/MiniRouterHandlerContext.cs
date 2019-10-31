#if NET45
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace jaytwo.MiniRouter.AspNet
{
    public class MiniRouterHandlerContext
    {
        public HttpContextBase HttpContext { get; set; }

        public string HandlerPath { get; set; }

        public string RelativePath { get; set; }
    }
}
#endif
