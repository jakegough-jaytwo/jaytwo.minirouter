using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaytwo.MiniRouter
{
    public class MiniHttpHandler : IMiniHttpHandler
    {
        private Func<MiniWebServerRequest, Task<MiniWebServerResponse>> _handlerDelegate;

        public MiniHttpHandler(Func<MiniWebServerRequest, Task<MiniWebServerResponse>> handlerDelegate)
        {
            _handlerDelegate = handlerDelegate;
        }

        public MiniHttpHandler(Func<MiniWebServerRequest, MiniWebServerResponse> handlerDelegate)
        {
            _handlerDelegate = x => Task.FromResult(handlerDelegate.Invoke(x));
        }

        public Task<MiniWebServerResponse> ProcessRequestAsync(MiniWebServerRequest request) => _handlerDelegate.Invoke(request);
    }
}
