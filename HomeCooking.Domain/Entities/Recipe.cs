using System.Collections.Generic;

namespace HomeCooking.Domain.Entities
{
    public class Recipe
    {
        public string Name { get;  set; }
        public string Method { get;  set; }
        public IEnumerable<Ingredient> Ingredients { get;  set; }
        public int Id { get; set; }
    }

    public struct Ingredient
    {
        public string FoodType { get;  set; }
        public Weight? Weight { get; set; }
        public Volume? Volume { get; set; }
        public Other? Other { get; set; }
    }

    public struct Other
    {
        public int Amount { get; }
        public string AmountType { get; }

        public Other(int amount, string amountType)
        {
            Amount = amount;
            AmountType = amountType;
        }
    }

    public struct Volume
    {
        public int Amount { get;  }
        public int Millilitres { get; }
        
        public Volume(int amount, int millilitres)
        {
            Amount = amount;
            Millilitres = millilitres;
        }
    }

    public struct Weight
    {
        public int Amount { get; private set; }
        public int Grams { get; private set; }
        
        public Weight(int amount, int grams)
        {
            Amount = amount;
            Grams = grams;
        }
    }
}