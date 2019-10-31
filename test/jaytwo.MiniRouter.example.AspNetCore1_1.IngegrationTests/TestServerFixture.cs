using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using jaytwo.SolutionResolution;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace jaytwo.MiniRouter.example.AspNetCore1_1.IngegrationTests
{
    public class TestServerFixture
    {
        private readonly TestServer _server;

        public TestServerFixture()
        {
            var contentRoot = new SlnFileResolver().ResolvePathRelativeToSln("examples/jaytwo.MiniRouter.example.AspNetCore1_1");

            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseContentRoot(contentRoot)
                .UseStartup<Startup>());
        }

        public HttpClient CreateClient() => _server.CreateClient();

        public void Dispose()
        {
            _server?.Dispose();
        }
    }
}
