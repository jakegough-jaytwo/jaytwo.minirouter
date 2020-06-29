#if NETSTANDARD || NETCOREAPP
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jaytwo.MiniRouter.AspNetCore;
using Microsoft.AspNetCore.Http;

namespace jaytwo.MiniRouter.example.AspNetCore
{
    public class HelloMiniRouterMiddleware : MiniRouterMiddleware
    {
        public HelloMiniRouterMiddleware(RequestDelegate next, string path = "hello")
            : base(next, path, new HelloMiniRouter())
        {
        }
    }
}
#endif
