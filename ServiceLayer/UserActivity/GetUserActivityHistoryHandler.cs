
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceLayer.UserActivity
{
    public class UserActivityHistoryRequest :IRequest<UserActivityHistoryResponse>
    {
        public int UserId { get; set; }
    }

    public class UserActivityHistoryResponse
    {
        public List<UserActivity> userActivities { get; set; }
    }

    public class UserActivity
    {
        public string Activity { get; set; }
        public int DurationInMinutes { get; set; }
    }

    //when your request requires a response
    public class GetUserActivityHistoryHandler : IRequestHandler<UserActivityHistoryRequest, UserActivityHistoryResponse>
    {
        public Task<UserActivityHistoryResponse> Handle(UserActivityHistoryRequest request, CancellationToken cancellationToken)
        {
            //TODO: send current user activities as response
            return Task.FromResult(new UserActivityHistoryResponse());
        }
    }
}
