using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jaytwo.MiniRouter
{
    public static class MiniWebServerRequestExtensions
    {
        public static async Task<string> GetBodyAsStringAsync(this MiniWebServerRequest request)
        {
            using (var reader = new StreamReader(request.Body))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
