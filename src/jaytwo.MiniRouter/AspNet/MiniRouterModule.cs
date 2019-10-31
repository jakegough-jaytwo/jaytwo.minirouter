#if NET45
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace jaytwo.MiniRouter.AspNet
{
    public class MiniRouterModule : IHttpModule
    {
        private readonly SemaphoreSlim _cachedHandlerSemapohre = new SemaphoreSlim(1);
        private readonly string _path;
        private readonly Func<IMiniRouterHandler> _handlerGenerator;

        private IMiniRouterHandler _cachedHandler;

        public MiniRouterModule(string path, IMiniRouter router)
            : this(path, () => new MiniRouterHandler(router))
        {
        }

        public MiniRouterModule(string path, Func<IMiniRouterHandler> handlerGenerator)
        {
            _path = path;
            _handlerGenerator = handlerGenerator;
        }

        public void Init(HttpApplication httpApplication)
        {
            var asyncHelper = new EventHandlerTaskAsyncHelper(async (sender, e) =>
            {
                var context = ((HttpApplication)sender).Context;
                var wrappedContext = new HttpContextWrapper(context);
                await ProcessRequestAsync(wrappedContext);
            });

            httpApplication.AddOnBeginRequestAsync(asyncHelper.BeginEventHandler, asyncHelper.EndEventHandler);
        }

        public async Task ProcessRequestAsync(HttpContextBase context)
        {
            var appRelativeCurrentExecutionFilePath = context.Request.CurrentExecutionFilePath.Substring(context.Request.ApplicationPath.Length).TrimStart('/');
            var currentRequestIsPath = string.Equals(appRelativeCurrentExecutionFilePath, _path, StringComparison.OrdinalIgnoreCase);
            var currentRequestStartsWithPath = appRelativeCurrentExecutionFilePath.StartsWith(_path + "/", StringComparison.OrdinalIgnoreCase);

            if (currentRequestIsPath || currentRequestStartsWithPath)
            {
                var miniRouterContext = new MiniRouterHandlerContext()
                {
                    HttpContext = context,
                    HandlerPath = _path,
                    RelativePath = appRelativeCurrentExecutionFilePath.Substring(_path.Length).TrimStart('/'),
                };

                var handler = await GetOrCreateHandlerAsync();
                await handler.ProcessRequestAsync(miniRouterContext);
            }
        }

        public void Dispose()
        {
        }

        private async Task<IMiniRouterHandler> GetOrCreateHandlerAsync()
        {
            await _cachedHandlerSemapohre.WaitAsync();
            try
            {
                if (_cachedHandler != null)
                {
                    return _cachedHandler;
                }
                else
                {
                    var handler = _handlerGenerator.Invoke();

                    if (handler.IsReusable)
                    {
                        _cachedHandler = handler;
                    }

                    return handler;
                }
            }
            finally
            {
                _cachedHandlerSemapohre.Release();
            }
        }
    }
}
#endif
