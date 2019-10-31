using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaytwo.MiniRouter
{
    public class MiniRouter : IMiniRouter
    {
        private readonly IList<IMiniRoute> _routes;

        public MiniRouter()
        {
            _routes = CreateRoutes().ToList();
        }

        public IMiniRoute GetRoute(MiniWebServerRequest request)
        {
            return _routes.FirstOrDefault(x => x.CanProcess(request));
        }

        protected virtual IEnumerable<IMiniRoute> CreateRoutes()
        {
            yield return new MiniRoute(
                canProcessDelegate: request => true,
                processRequestDelegate: request =>
                {
                    return Task.FromResult(new MiniWebServerResponse()
                    {
                        Body = Encoding.UTF8.GetBytes($"{nameof(MiniRouter)} OK"),
                        ContentType = "text/plain",
                        StatusCode = 200,
                    });
                });
        }
    }
}
