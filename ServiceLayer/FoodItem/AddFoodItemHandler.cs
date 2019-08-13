using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceLayer.FoodItem
{
    public class FoodItemRequest : IRequest<bool>
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

    public class AddFoodItemHandler : IRequestHandler<FoodItemRequest, bool>
    {
        public Task<bool> Handle(FoodItemRequest request, CancellationToken cancellationToken)
        {
            //1.Validate 'request' using fluent validation

            //2.perform bussiness validation -- Food item exists

            //3. Save to Database
            return Task.FromResult(true);
        }
    }
}
