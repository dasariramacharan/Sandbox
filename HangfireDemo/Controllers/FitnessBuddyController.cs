using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.FoodItem;
using ServiceLayer.Messages;

namespace HangfireDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FitnessBuddyController : Controller
    {
        private readonly IMediator _mediator;
        public FitnessBuddyController(IMediator mediator)
        {
            _mediator = mediator;
        }


        public void AddAMeal(FoodTakenMessage foodTaken)
        {
            //TODO: Add tests for Notificaiton message type
            //e.g _mediator.Publish(new FoodTakenMessage { FoodItem = "Chapathi", QuantityConsumedInGrams = 50 });
            _mediator.Publish(foodTaken);
        }

        public bool AddFoodItem(FoodItemRequest foodItem)
        {
            return _mediator.Send(foodItem).Result;
        }
    }
}