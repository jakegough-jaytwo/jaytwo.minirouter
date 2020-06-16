using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace jaytwo.MiniRouter
{
    public static class MiniWebServerResponseExtensions
    {
        public static MiniWebServerResponse WithStatusCodeOK(this MiniWebServerResponse response)
        {
            return response.WithStatusCode(HttpStatusCode.OK);
        }

        public static MiniWebServerResponse WithStatusCodeBadRequest(this MiniWebServerResponse response)
        {
            return response.WithStatusCode(HttpStatusCode.BadRequest);
        }

        public static MiniWebServerResponse WithStatusCodeNotFound(this MiniWebServerResponse response)
        {
            return response.WithStatusCode(HttpStatusCode.NotFound);
        }

        public static MiniWebServerResponse WithStatusCodeUnauthorized(this MiniWebServerResponse response)
        {
            return response.WithStatusCode(HttpStatusCode.Unauthorized);
        }

        public static MiniWebServerResponse WithStatusCodeForbidden(this MiniWebServerResponse response)
        {
            return response.WithStatusCode(HttpStatusCode.Forbidden);
        }

        public static MiniWebServerResponse WithStatusCode(this MiniWebServerResponse response, HttpStatusCode httpStatusCode)
        {
            return response.WithStatusCode((int)httpStatusCode);
        }

        public static MiniWebServerResponse WithStatusCode(this MiniWebServerResponse response, int httpStatusCode)
        {
            response.StatusCode = httpStatusCode;
            return response;
        }

        public static MiniWebServerResponse WithBody(this MiniWebServerResponse response, byte[] content)
        {
            response.Body = content;
            return response;
        }

        public static MiniWebServerResponse WithBody(this MiniWebServerResponse response, string content)
        {
            return response.WithBody(Encoding.UTF8.GetBytes(content));
        }

        public static MiniWebServerResponse WithBodyJson(this MiniWebServerResponse response, object content)
        {
            var json = JsonConvert.SerializeObject(content);

            return response
                .WithBody(json)
                .WithContentTypeApplicationJson();
        }

        public static MiniWebServerResponse WithContentType(this MiniWebServerResponse response, string contentType)
        {
            response.ContentType = contentType;
            return response;
        }

        public static MiniWebServerResponse WithContentTypeApplicationJson(this MiniWebServerResponse response)
        {
            return response.WithContentType("application/json");
        }

        public static MiniWebServerResponse WithContentTypeApplicationOctetStream(this MiniWebServerResponse response)
        {
            return response.WithContentType("application/octet-stream");
        }

        public static MiniWebServerResponse WithContentTypeApplicationXml(this MiniWebServerResponse response)
        {
            return response.WithContentType("application/xml");
        }

        public static MiniWebServerResponse WithContentTypeImageGif(this MiniWebServerResponse response)
        {
            return response.WithContentType("image/gif");
        }

        public static MiniWebServerResponse WithContentTypeImageJpeg(this MiniWebServerResponse response)
        {
            return response.WithContentType("image/jpeg");
        }

        public static MiniWebServerResponse WithContentTypeTextPlain(this MiniWebServerResponse response)
        {
            return response.WithContentType("text/plain");
        }

        public static MiniWebServerResponse WithContentTypeTextHtml(this MiniWebServerResponse response)
        {
            return response.WithContentType("text/html");
        }

        public static MiniWebServerResponse WithContentTypeTextXml(this MiniWebServerResponse response)
        {
            return response.WithContentType("text/xml");
        }

        public static string GetHeaderValue(this MiniWebServerResponse response, string key)
        {
            return response.GetHeaderValues(key)?.FirstOrDefault();
        }

        public static string[] GetHeaderValues(this MiniWebServerResponse response, string key)
        {
            string[] values = null;
            response?.Headers?.TryGetValue(key, out values);
            return values;
        }

        public static MiniWebServerResponse AddHeader(this MiniWebServerResponse response, string key, string value)
        {
            return response.AddHeader(key, new[] { value });
        }

        public static MiniWebServerResponse AddHeader(this MiniWebServerResponse response, string key, string[] values)
        {
            var list = new List<string>(response.GetHeaderValues(key) ?? new string[] { });
            list.AddRange(values);
            response.Headers[key] = list.ToArray();
            return response;
        }

        public static MiniWebServerResponse SetHeader(this MiniWebServerResponse response, string key, string value)
        {
            return response.SetHeader(key, new[] { value });
        }

        public static MiniWebServerResponse SetHeader(this MiniWebServerResponse response, string key, string[] values)
        {
            response.Headers[key] = values;
            return response;
        }
    }
}
