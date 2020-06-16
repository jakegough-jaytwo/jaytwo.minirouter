using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace jaytwo.MiniRouter
{
    public class MiniWebServerRequest
    {
        public IDictionary<string, string[]> Headers { get; set; }

        public string Method { get; set; }

        public string Host { get; set; }

        public string Path { get; set; }

        public string RelativePath { get; set; }

        public string Query { get; set; }

        public Stream Body { get; set; }
    }
}
