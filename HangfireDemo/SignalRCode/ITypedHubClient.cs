using System.Threading.Tasks;

namespace Playground.Web.SignalRCode
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(string type, string payload);
        Task ReceiveAddLaterResult(int result);
    }
}
