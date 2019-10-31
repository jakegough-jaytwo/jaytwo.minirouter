using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaytwo.MiniRouter
{
    public interface IMiniRoute
    {
        Task<MiniWebServerResponse> ProcessAsync(MiniWebServerRequest request);

        bool CanProcess(MiniWebServerRequest request);
    }
}
