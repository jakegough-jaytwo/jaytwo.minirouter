#if NETCORE
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace jaytwo.MiniRouter.AspNetCore
{
    public class MiniRouterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _path;
        private readonly IMiniRouter _miniRouter;

        public MiniRouterMiddleware(RequestDelegate next, string path, IMiniRouter miniRouter)
        {
            _next = next;
            _path = path;
            _miniRouter = miniRouter;
        }

        public async Task Invoke(HttpContext context)
        {
            var currentRequestPath = context.Request.Path.ToString().TrimStart('/');
            var currentRequestIsPath = string.Equals(currentRequestPath, _path, StringComparison.OrdinalIgnoreCase);
            var currentRequestStartsWithPath = currentRequestPath.StartsWith(_path + "/", StringComparison.OrdinalIgnoreCase);

            if (currentRequestIsPath || currentRequestStartsWithPath)
            {
                var relativePath = currentRequestPath.Substring(_path.Length).TrimStart('/');
                var request = GetMiniWebServerRequest(context.Request, relativePath);
                var miniRoute = _miniRouter.GetRoute(request);

                context.Response.Headers.Add("__minirouter", _path);

                if (miniRoute != null)
                {
                    var stopwatch = Stopwatch.StartNew();
                    var result = await miniRoute.ProcessAsync(request);
                    stopwatch.Stop();

                    context.Response.Headers.Add("__minirouter_duration_ms", stopwatch.ElapsedMilliseconds.ToString("n2"));

                    await WriteResponseAsync(context.Response, result);
                }
                else
                {
                    context.Response.StatusCode = 404;
                    context.Response.ContentType = "text/plain";

                    using (var writer = new StreamWriter(context.Response.Body))
                    {
                        await writer.WriteAsync("Not Found");
                    }
                }
            }
            else
            {
                await _next.Invoke(context);
            }
        }

        private async Task WriteResponseAsync(HttpResponse httpResponse, MiniWebServerResponse miniRouterResponse)
        {
            httpResponse.StatusCode = miniRouterResponse.StatusCode;
            httpResponse.ContentType = miniRouterResponse.ContentType;

            if (miniRouterResponse.Headers != null)
            {
                foreach (var header in miniRouterResponse.Headers)
                {
                    httpResponse.Headers[header.Key] = header.Value;
                }
            }

            await httpResponse.Body.WriteAsync(miniRouterResponse.Body, 0, miniRouterResponse.Body.Length);
        }

        private MiniWebServerRequest GetMiniWebServerRequest(HttpRequest httpRequest, string relativePath)
        {
            var request = new MiniWebServerRequest()
            {
                Host = httpRequest.Host.ToString(),
                Method = httpRequest.Method,
                Path = httpRequest.Path,
                Query = httpRequest.QueryString.Value,
                RelativePath = relativePath,
                Body = httpRequest.Body,
            };

            if (httpRequest.Headers != null)
            {
                request.Headers = new Dictionary<string, string[]>();

                foreach (var header in httpRequest.Headers)
                {
                    request.Headers[header.Key] = header.Value;
                }
            }

            return request;
        }
    }
}
#endif
