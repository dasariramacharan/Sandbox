using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Messages
{
    public class FoodTakenMessage : INotification
    {
        //TODO: Change to FoodItemId
        public string FoodItem { get; set; }

        //TODO:Separate to quantity and UnitType
        public int QuantityConsumedInGrams { get; set; }
    }
}
