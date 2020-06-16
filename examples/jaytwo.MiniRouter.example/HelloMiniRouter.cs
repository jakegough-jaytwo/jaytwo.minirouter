using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaytwo.MiniRouter.example
{
    public class HelloMiniRouter : MiniRouter
    {
        public HelloMiniRouter()
        {
        }

        protected override IEnumerable<IMiniRoute> CreateRoutes()
        {
            var imageHandler = new ImageHandler();

            yield return new MiniRoute(
                method: "get",
                pathPrefix: "image",
                handler: imageHandler);

            var xmlHandler = new XmlHandler();

            yield return new MiniRoute(
                method: "GET",
                pathPrefix: "xml",
                handler: xmlHandler);

            yield return new MiniRoute(
                method: "GET",
                headers: new[] { new KeyValuePair<string, string>("Accept", "application/xml") },
                handler: xmlHandler);

            var jsonHandler = new JsonHandler();

            yield return new MiniRoute(
                methods: new[] { "GET" },
                pathPrefix: "json",
                handler: jsonHandler);

            yield return new MiniRoute(
                methods: new[] { "GET" },
                headers: new[] { new KeyValuePair<string, string>("Accept", "application/json") },
                handler: jsonHandler);

            // default route last
            yield return new MiniRoute(handler: new DefaultHandler());
        }
    }
}
