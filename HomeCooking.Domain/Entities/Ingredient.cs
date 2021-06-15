using System;

namespace HomeCooking.Domain.Entities
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Item { get;  set; }
        public double Amount { get; set; }
    }
}