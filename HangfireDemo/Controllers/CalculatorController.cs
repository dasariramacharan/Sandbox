using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Playground.Web.SignalRCode;

namespace Playground.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : Controller
    {
        private IBackgroundJobClient _jobClient;
        private IHubContext<NotifyHub, ITypedHubClient> _hubContext;

        public CalculatorController(IBackgroundJobClient jobClient, IHubContext<NotifyHub, ITypedHubClient> hubContext)
        {
            _jobClient = jobClient;
            _hubContext = hubContext;
        }

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public int AddNumbers(int a, int b)
        {
            return a + b;
        }

        [HttpPost("[action]")]
        public void AddLater(AddNumbersRequest request)
        {
            //result sent when asked using Signalr - Hub.RequestAddLaterResult
            return;

            //below code for hangfire
            //_jobClient.Enqueue(() => AddNumberAndNotify(request));
        }

        //public void AddNumberAndNotify(AddNumbersRequest request)
        //{
        //    //Thread.Sleep(3000);
        //    var result = request.a + request.b;

        //    //_hubContext.Clients.All.BroadcastMessage($"Result of add notify of {request.a} and {request.b}", result.ToString());
        //}
    }

    public class AddNumbersRequest
    {
        public int a { get; set; }
        public int b { get; set; }
    }
}