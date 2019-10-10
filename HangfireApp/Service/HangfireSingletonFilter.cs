using Hangfire.Client;
using Hangfire.Common;
using Hangfire.States;
using Newtonsoft.Json;
using System.Linq;

namespace HangfireApp.Service
{
    /// <summary>
    /// Prevent a method from being created if already queued or in execution
    /// Note: set `varybyArgs` false to allow only 1 instance of method to run irrespective of the params to that method  
    /// </summary>
    /// <remarks>
    ///  It does comparision of serialised params text so there are exceptions like {1,2} is not the same as {2,1} and hence can run at the same time  
    /// </remarks>
    internal class HangfireSingletonFilter : JobFilterAttribute, IClientFilter
    {
        private bool VarybyArgs;

        /// <param name="varybyArgs">default True</param>
        public HangfireSingletonFilter(bool varybyArgs = true)
        {
            VarybyArgs = varybyArgs;
        }

        public void OnCreating(CreatingContext context)
        {
            var methodName = context.Job.Method.Name;
            CancelJobIfAlreadyInProcessingState(context, methodName, VarybyArgs);

            if (!context.Canceled)
                CancelJobIfAlreadyInQueue(context, methodName, VarybyArgs);
        }

        private static void CancelJobIfAlreadyInQueue(CreatingContext context, string methodName, bool varyByArgs)
        {
            var queueName = ((EnqueuedState)context.InitialState).Queue;
            var api = context.Storage.GetMonitoringApi();
            var count = api.EnqueuedCount(queueName);
            var jobs = api.EnqueuedJobs(queueName, 0, (int)count);
            var currentJobHash = GetArgssHash(context.Job);

            if (jobs.Any(j =>
            {
                var jv = j.Value; return jv.Job.Method.Name == methodName && jv.InEnqueuedState && (!varyByArgs || GetArgssHash(jv.Job) == currentJobHash);
            }))
            {
                context.Canceled = true;
            }
        }

        private static void CancelJobIfAlreadyInProcessingState(CreatingContext context, string methodName, bool varyByArgs)
        {
            var api = context.Storage.GetMonitoringApi();
            var count = api.ProcessingCount();
            var jobs = api.ProcessingJobs(0, (int)count);
            var currentJobHash = GetArgssHash(context.Job);

            if (jobs.Any(j =>
            {
                var jv = j.Value; return jv.Job.Method.Name == methodName && jv.InProcessingState && (!varyByArgs || GetArgssHash(jv.Job) == currentJobHash);
            }))
            {
                context.Canceled = true;
            }
        }

        private static string GetArgssHash(Job job)
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
