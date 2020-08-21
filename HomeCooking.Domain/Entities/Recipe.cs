using System.Collections.Generic;

namespace HomeCooking.Domain.Entities
{
    public class Recipe
    {
        public Recipe()
        {
            Ingredients = new List<Ingredient>();
        }
        public string Name { get;  set; }
        public string Method { get;  set; }
        public IEnumerable<Ingredient> Ingredients { get;  set; }
        public int Id { get; set; }
    }

    public class Ingredient
    {
        public int Id { get; set; }
        public string FoodType { get;  set; }
        public Weight Weight { get; set; }
        public Volume Volume { get; set; }
        public Other Other { get; set; }
    }

    public class Other
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string AmountType { get; set; }
        
    }

    public class Volume
    {
        public int Id { get; set; }
        public int Amount { get;  }
        public int Millilitres { get; set; }
    }

    public class Weight
    {
        public int Id { get; set; }
        public int Amount { get;  set; }
        public int Grams { get; set; }
        
    }
}