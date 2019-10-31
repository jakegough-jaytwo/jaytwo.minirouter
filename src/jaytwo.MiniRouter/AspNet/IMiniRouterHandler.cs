#if NET45
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace jaytwo.MiniRouter.AspNet
{
    public interface IMiniRouterHandler : IHttpHandler
    {
        Task ProcessRequestAsync(MiniRouterHandlerContext context);
    }
}
#endif
