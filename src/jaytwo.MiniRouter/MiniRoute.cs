using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace jaytwo.MiniRouter
{
    public class MiniRoute : IMiniRoute
    {
        public MiniRoute(
            IMiniHttpHandler handler = null,
            string hostPattern = null,
            string pathPrefix = null,
            string[] methods = null,
            string method = null,
            KeyValuePair<string, string>[] headers = null)
        {
            Handler = handler;
            HostPattern = hostPattern;
            PathPrefix = pathPrefix;
            Methods = method != null ? new[] { method } : methods;
            Headers = headers;
        }

        public IMiniHttpHandler Handler { get; set; }

        public string HostPattern { get; set; }

        public string PathPrefix { get; set; }

        public string[] Methods { get; set; }

        public KeyValuePair<string, string>[] Headers { get; set; }

        public KeyValuePair<string, string>[] Queries { get; set; }

        public Func<MiniWebServerRequest, bool>[] MatchDelegates { get; set; }

        public bool CanProcess(MiniWebServerRequest request)
        {
            return Handler != null
                && HostPatternMatches(request.Host)
                && PathPrefixMatches(request.Path)
                && MethodMatches(request.Method)
                && HeaderMatches(request.Headers)
                && QueryMatches(request.Query)
                && DelegateMatches(request);
        }

        public Task<MiniWebServerResponse> ProcessAsync(MiniWebServerRequest request)
        {
            return Handler?.ProcessRequestAsync(request);
        }

        internal bool HostPatternMatches(string host)
        {
            if (string.IsNullOrEmpty(HostPattern))
            {
                return true;
            }

            return Regex.IsMatch(host, HostPattern);
        }

        internal bool PathPrefixMatches(string path)
        {
            if (string.IsNullOrEmpty(PathPrefix))
            {
                return true;
            }

            return Regex.IsMatch(path, PathPrefix);
        }

        internal bool MethodMatches(string method)
        {
            if (Methods == null || !Methods.Any())
            {
                return true;
            }

            foreach (var matchMethod in Methods)
            {
                if (string.Equals(matchMethod, method, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        internal bool HeaderMatches(IDictionary<string, string[]> headers)
        {
            if (Headers == null || !Headers.Any())
            {
                return true;
            }

            foreach (var header in headers)
            {
                foreach (var headerValue in header.Value)
                {
                    foreach (var matchHeader in Headers)
                    {
                        if (header.Key == matchHeader.Key && headerValue == matchHeader.Value)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        internal bool QueryMatches(string query)
        {
            return true;
        }

        internal bool DelegateMatches(MiniWebServerRequest request)
        {
            if (MatchDelegates == null || !MatchDelegates.Any())
            {
                return true;
            }

            foreach (var matchDelegate in MatchDelegates)
            {
                if (matchDelegate.Invoke(request))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
