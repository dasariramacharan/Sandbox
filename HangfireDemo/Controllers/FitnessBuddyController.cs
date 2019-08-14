using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Auth;
using ServiceLayer.FoodItem;
using ServiceLayer.Messages;
using ServiceLayer.UserActivity;
using System.Threading.Tasks;

namespace Playground.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FitnessBuddyController : Controller
    {
        private readonly IMediator _mediator;
        public int LoggedInUserId { get; set; }
        public FitnessBuddyController(IMediator mediator)
        {
            _mediator = mediator;
            LoggedInUserId = 100;
        }


        public void AddAMeal(FoodTakenMessage foodTaken)
        {
            //TODO: Add tests for Notificaiton message type
            //e.g _mediator.Publish(new FoodTakenMessage { FoodItem = "Chapathi", QuantityConsumedInGrams = 50 });
            _mediator.Publish(foodTaken);
        }

        public async Task AddFoodItemAsync(FoodItemRequest foodItem)
        {
            //TODO: Add tests 
            await _mediator.Send(foodItem);
        }

        public async Task<UserActivityHistoryResponse> GetUserActivityHistoryAsync()
        {
            //TODO: Add tests 
            return await _mediator.Send(new UserActivityHistoryRequest { UserId = LoggedInUserId });
        }

        //Syncronous call. but internally async anyway
        public bool IsValidUser(LoginCredentials loginCredentials)
        {
            //TODO: Add tests 
            return _mediator.Send(loginCredentials).Result;
        }
    }
}