using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            //e.g _mediator.Publish(new FoodTakenMessage { FoodItem = "Chapathi", QuantityConsumedInGrams = 50 });
            _mediator.Publish(foodTaken);
        }
    }
}