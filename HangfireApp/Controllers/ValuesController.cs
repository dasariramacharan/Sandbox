﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using HangfireApp.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace HangfireApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IBackgroundJobClient _jobClient;

        public ValuesController(IBackgroundJobClient jobClient)
        {
            _jobClient = jobClient;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get(int a, int b)
        {
            _jobClient.Enqueue(() => new CalculatorService().Add(new CalculatorService.AddRequest { a = a, b = b }));
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpGet]
        public ActionResult<string> StartASchedule()
        {
            var every10SecCronEx = "0/59 * * * * *";
            RecurringJob.AddOrUpdate<CalculatorService>("Add",
              mdus => mdus.Add( new CalculatorService.AddRequest { a = 300, b = 400 }), every10SecCronEx);
            return "schedule created";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
