using System;
using System.Collections.Generic;
using System.Text;

namespace jaytwo.MiniRouter
{
    public class MiniWebServerResponse
    {
        public IDictionary<string, string> Headers { get; set; }

        public byte[] Body { get; set; }

        public int StatusCode { get; set; }

        public string ContentType { get; set; }
    }
}
