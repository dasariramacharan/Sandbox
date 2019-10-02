using Playground.Web.Controllers;
using System;
using System.Threading;

namespace Playground.Web.Services
{
    public class CalculatorService
    {
        //[DisableConcurrentExecution(timeoutInSeconds:10)] -- kills job if it exceeds given time
        [HangfireDisableMultipleQueuedItemsFilter]
        public void Add(AddNumbersRequest request)
        {
            //int numberofSeconds = 20;
            //Thread.Sleep(numberofSeconds * 1000);
            Console.WriteLine($"ADD {request.a} and {request.b} is {request.a + request.b}");
        }
    }
}
