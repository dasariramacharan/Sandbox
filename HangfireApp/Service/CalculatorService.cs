using HangfireApp.Service;
using System;
using System.Diagnostics;
using System.Threading;

namespace HangfireApp.Web.Services
{
    public class CalculatorService
    {
        //[DisableConcurrentExecution(timeoutInSeconds:10)] -- kills job if it exceeds given time
        [HangfireSingletonFilter] //-- vary by parameter
        public void AddDoNotIgnoreParams(AddRequest request)
        {
            int numberofSeconds = 200;
            Thread.Sleep(numberofSeconds * 1000);

            var a = request.a;
            var b = request.b;

            Console.WriteLine($"ADD {a} and {b} is {a + b}");
            Debug.WriteLine($"ADD {a} and {b} is {a + b}");
        }


        [HangfireSingletonFilter(true)] //-- do not vary by parameter
        public void AddIgnoreParams(AddRequest request)
        {
            int numberofSeconds = 200;
            Thread.Sleep(numberofSeconds * 1000);

            var a = request.a;
            var b = request.b;

            Console.WriteLine($"ADD {a} and {b} is {a + b}");
            Debug.WriteLine($"ADD {a} and {b} is {a + b}");
        }

        public class AddRequest
        {
            public int a { get; set; }
            public int b { get; set; }

        }
    }
}
