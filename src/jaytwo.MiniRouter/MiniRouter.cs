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
                handler: new MiniHttpHandler(request =>
                    new MiniWebServerResponse()
                        .WithStatusCodeOK()
                        .WithContentTypeTextPlain()
                        .WithBody($"{nameof(MiniRouter)} OK")));
        }
    }
}
