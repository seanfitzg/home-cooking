using System.Collections.Generic;

namespace HomeCooking.Domain.Entities
{
    public class Recipe
    {
        public Recipe()
        {
            Ingredients = new List<Ingredient>();
        }

        public string UserId { get; set; }
        public string Name { get;  set; }
        public string Method { get;  set; }
        public string Description { get;  set; }
        public IList<Ingredient> Ingredients { get;  set; }
        public int Id { get; set; }
    }
}