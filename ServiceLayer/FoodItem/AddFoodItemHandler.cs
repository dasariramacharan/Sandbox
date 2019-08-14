using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceLayer.FoodItem
{
    public class FoodItemRequest : IRequest
    {
        public int FoodItemId { get; set; }
        public string Name { get; set; }
        public int CaloriesPerGm { get; set; }
        public List<Nutrient> Nutrients { get; set; }
    }

    public class Nutrient
    {
        public string NutrientName { get; set; }//TODO: suggest to be enum
        public int QuantityPerGm { get; set; }
    }
    
    // your message does not require a response, use the AsyncRequestHandler<TRequest> base class
    public class AddFoodItemHandler : AsyncRequestHandler<FoodItemRequest>
    {
        protected override Task Handle(FoodItemRequest request, CancellationToken cancellationToken)
        {
            //1.Validate 'request' using fluent validation

            //2.perform bussiness validation -- Food item exists

            //3. Save to Database
            return Task.FromResult(true);
        }
    }
}
