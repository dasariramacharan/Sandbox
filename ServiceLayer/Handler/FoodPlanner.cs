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
    public class FoodPlanner : INotificationHandler<FoodTakenMessage>
    {
        public Task Handle(FoodTakenMessage notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Save nutrients received for the day and update the rest of food plan for today");
            return Task.CompletedTask;
        }
    }
}
