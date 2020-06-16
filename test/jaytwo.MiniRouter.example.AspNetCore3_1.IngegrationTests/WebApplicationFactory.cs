using System;
using System.IO;
using System.Linq;
using jaytwo.SolutionResolution;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace jaytwo.MiniRouter.example.AspNetCore3_1.IngegrationTests
{
    public class WebApplicationFactory
        : WebApplicationFactory<example.AspNetCore3_1.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");

            var contentRoot = new SlnFileResolver().ResolvePathRelativeToSln("examples/jaytwo.MiniRouter.example.AspNetCore2_1");
            builder.UseContentRoot(contentRoot);
        }
    }
}
