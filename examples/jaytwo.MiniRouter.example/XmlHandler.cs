using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaytwo.MiniRouter.example
{
    public class XmlHandler : IMiniHttpHandler
    {
        public Task<MiniWebServerResponse> ProcessRequestAsync(MiniWebServerRequest request)
        {
            var xml = @"<?xml version=""1.0"" encoding=""UTF-8""?><text><para>hello world</para></text>";

            var response = new MiniWebServerResponse()
                .WithStatusCodeOK()
                .WithBody(xml)
                .WithContentTypeApplicationXml();

            return Task.FromResult(response);
        }
    }
}
