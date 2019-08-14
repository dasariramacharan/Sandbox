using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Playground.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : Controller
    {
        private IBackgroundJobClient _jobClient;

        public CalculatorController(IBackgroundJobClient jobClient)
        {
            _jobClient = jobClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public int AddNumbers(int a, int b)
        {
            return a + b;
        }

        [HttpGet("[action]")]
        public void AddLater(int a, int b)
        {
            _jobClient.Enqueue(() => AddNumbers(a, b));
        }
    }
}