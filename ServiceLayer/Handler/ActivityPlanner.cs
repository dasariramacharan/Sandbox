using MediatR;
using ServiceLayer.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceLayer.Handler
{
    public class ActivityPlanner : INotificationHandler<FoodTakenMessage>

    {
        public Task Handle(FoodTakenMessage notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Save calories added for today and update activity required for today");
            return Task.CompletedTask;
        }
    }
}
