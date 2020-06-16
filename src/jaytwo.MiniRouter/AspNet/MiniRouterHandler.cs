#if NET45
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace jaytwo.MiniRouter.AspNet
{
    public class MiniRouterHandler : HttpTaskAsyncHandler, IMiniRouterHandler, IHttpHandler
    {
        private readonly IMiniRouter _router;

        public MiniRouterHandler(IMiniRouter router)
        {
            _router = router;
        }

        public override bool IsReusable => true;

        public override Task ProcessRequestAsync(HttpContext context)
        {
            var wrappedContext = new HttpContextWrapper(context);
            return ProcessRequestAsync(wrappedContext);
        }

        public Task ProcessRequestAsync(HttpContextBase context)
        {
            var handlerPath = context.Request.CurrentExecutionFilePath.Substring(context.Request.ApplicationPath.Length).TrimStart('/');
            var relativePathAndQuery = context.Request.Url.PathAndQuery.Substring(context.Request.CurrentExecutionFilePath.Length).TrimStart('/');
            var relativePath = relativePathAndQuery.Split('?')[0];

            var miniRouterContext = new MiniRouterHandlerContext()
            {
                HttpContext = context,
                HandlerPath = handlerPath,
                RelativePath = relativePath,
            };

            return ProcessRequestAsync(miniRouterContext);
        }

        public async Task ProcessRequestAsync(MiniRouterHandlerContext miniRouterContext)
        {
            var httpRequest = miniRouterContext.HttpContext.Request;

            var miniRouterRequest = new MiniWebServerRequest()
            {
                Host = httpRequest.Url.Host,
                Method = httpRequest.HttpMethod,
                Path = httpRequest.Path,
                RelativePath = miniRouterContext.RelativePath,
                Query = httpRequest.Url.Query,
                Body = httpRequest.InputStream,
            };

            if (httpRequest.Headers != null)
            {
                miniRouterRequest.Headers = new Dictionary<string, string[]>();

                foreach (var headerKey in httpRequest.Headers.AllKeys)
                {
                    miniRouterRequest.Headers[headerKey] = httpRequest.Headers.GetValues(headerKey);
                }
            }

            var httpResponse = miniRouterContext.HttpContext.Response;
            httpResponse.AddHeader("__minirouter", miniRouterContext.HandlerPath);

            var route = _router.GetRoute(miniRouterRequest);
            if (route != null)
            {
                var stopwatch = Stopwatch.StartNew();
                var result = await route.ProcessAsync(miniRouterRequest);
                stopwatch.Stop();

                httpResponse.AddHeader("__minirouter_duration_ms", stopwatch.ElapsedMilliseconds.ToString("n2"));

                httpResponse.StatusCode = result.StatusCode;
                httpResponse.ContentType = result.ContentType;

                if (result.Headers != null)
                {
                    foreach (var header in result.Headers)
                    {
                        foreach (var value in header.Value)
                        {
                            httpResponse.Headers.Add(header.Key, value);
                        }
                    }
                }

                httpResponse.BinaryWrite(result.Body);
            }
            else
            {
                httpResponse.StatusCode = 404;
                httpResponse.ContentType = "text/plain";
                httpResponse.Write("Not Found");
            }

            httpResponse.End();
        }
    }
}
#endif
