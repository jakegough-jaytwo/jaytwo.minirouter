#if NETSTANDARD || NETCOREAPP
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace jaytwo.MiniRouter.AspNetCore
{
    public class MiniRouterMiddlewareContext
    {
        public HttpContext HttpContext { get; set; }

        public string HandlerPath { get; set; }

        public string RelativePath { get; set; }
    }
}
#endif
