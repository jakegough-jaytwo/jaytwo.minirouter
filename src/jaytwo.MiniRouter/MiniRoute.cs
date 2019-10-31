using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace jaytwo.MiniRouter
{
    public class MiniRoute : IMiniRoute
    {
        private readonly Func<MiniWebServerRequest, bool> _canProcessDelegate;
        private readonly Func<MiniWebServerRequest, Task<MiniWebServerResponse>> _processRequestDelegate;

        public MiniRoute(Func<MiniWebServerRequest, bool> canProcessDelegate, Func<MiniWebServerRequest, Task<MiniWebServerResponse>> processRequestDelegate)
        {
            _canProcessDelegate = canProcessDelegate;
            _processRequestDelegate = processRequestDelegate;
        }

        public bool CanProcess(MiniWebServerRequest request)
        {
            return _canProcessDelegate(request);
        }

        public Task<MiniWebServerResponse> ProcessAsync(MiniWebServerRequest request)
        {
            return _processRequestDelegate(request);
        }
    }
}
