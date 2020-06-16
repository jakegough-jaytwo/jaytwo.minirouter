using System.Threading.Tasks;

namespace jaytwo.MiniRouter
{
    public interface IMiniHttpHandler
    {
        Task<MiniWebServerResponse> ProcessRequestAsync(MiniWebServerRequest request);
    }
}
