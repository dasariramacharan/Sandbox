using Hangfire.Client;
using Hangfire.Common;
using Hangfire.States;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace HangfireApp.Service
{
    /// <summary>
    /// Prevent a method with same parameters running more than once
    /// </summary>
    /// <remarks>
    ///  It does comparision of serialised params text so there are exceptions like {1,2} is not the same as {2,1} and hence can run at the same time  
    /// </remarks>
    internal class HangfireSingletonFilter : JobFilterAttribute, IClientFilter
    {
        public void OnCreating(CreatingContext context)
        {
            var methodName = context.Job.Method.Name;
            CancelJobIfAlreadyInProcessingState(context, methodName);
            
            if (!context.Canceled)
                CancelJobIfAlreadyInQueue(context, methodName);

        }

        private static void CancelJobIfAlreadyInQueue(CreatingContext context, string methodName)
        {
            var queueName = ((EnqueuedState)context.InitialState).Queue;
            var api = context.Storage.GetMonitoringApi();
            var count = api.EnqueuedCount(queueName);
            var jobs = api.EnqueuedJobs(queueName, 0, (int)count);
            var currentJobHash = GetParamsHash(context.Job);

            if (jobs.Any(j => { var jv = j.Value; return jv.Job.Method.Name == methodName && jv.InEnqueuedState && GetParamsHash(jv.Job) == currentJobHash; }))
                context.Canceled = true;
        }

        private static void CancelJobIfAlreadyInProcessingState(CreatingContext context, string methodName)
        {
            var api = context.Storage.GetMonitoringApi();
            var count = api.ProcessingCount();
            var jobs = api.ProcessingJobs(0, (int)count);
            var currentJobHash = GetParamsHash(context.Job);

            if (jobs.Any(j => { var jv = j.Value; return jv.Job.Method.Name == methodName && jv.InProcessingState && GetParamsHash(jv.Job) == currentJobHash; }))
                context.Canceled = true;
        }

        private static string GetParamsHash(Job job)
        {
            if (job?.Args != null)
                return JsonConvert.SerializeObject(job.Args);
            return string.Empty;
        }

        void IClientFilter.OnCreated(CreatedContext context)
        {
        }
    }
}
