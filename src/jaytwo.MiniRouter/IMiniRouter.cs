using System;
using System.Collections.Generic;
using System.Text;

namespace jaytwo.MiniRouter
{
    public interface IMiniRouter
    {
        IMiniRoute GetRoute(MiniWebServerRequest request);
    }
}
