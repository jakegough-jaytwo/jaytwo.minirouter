using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaytwo.MiniRouter.example
{
    public class DefaultHandler : IMiniHttpHandler
    {
        public Task<MiniWebServerResponse> ProcessRequestAsync(MiniWebServerRequest request)
        {
            return Task.FromResult(new MiniWebServerResponse()
            {
                Body = Encoding.UTF8.GetBytes($"Hello, world! (from {nameof(HelloMiniRouter)})"),
                ContentType = "text/plain",
                StatusCode = 200,
            });
        }
    }
}
