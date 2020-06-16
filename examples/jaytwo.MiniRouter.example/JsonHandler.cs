using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaytwo.MiniRouter.example
{
    public class JsonHandler : IMiniHttpHandler
    {
        public Task<MiniWebServerResponse> ProcessRequestAsync(MiniWebServerRequest request)
        {
            var response = new MiniWebServerResponse()
                .WithStatusCodeOK()
                .WithBodyJson(new
                {
                    text = new
                    {
                        para = "hello world",
                    },
                });

            return Task.FromResult(response);
        }
    }
}
