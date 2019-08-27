using Microsoft.AspNetCore.SignalR;
using Playground.Web.Controllers;
using System.Threading.Tasks;

namespace Playground.Web.SignalRCode
{
    public class NotifyHub : Hub<ITypedHubClient>
    {
        public async Task SendBroadcastMessage(string type, string payload)
        {
            await Clients.All.BroadcastMessage(type, payload);
        }

        public async Task RequestAddLaterResult(AddNumbersRequest request)
        {
            await Clients.Caller.ReceiveAddLaterResult(request.a + request.b);
        }
    }
}
