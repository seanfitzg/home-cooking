using System.Collections.Generic;
using HomeCooking.Domain.Entities;

namespace HomeCooking.Application.DTOs
{
    public class RecipeDto
    {
        public RecipeDto()
        {
            
        }
        
        public RecipeDto(Recipe recipe)
        {
            Id = recipe.Id;
            Name = recipe.Name;
            Method = recipe.Method;
            Description = recipe.Description;
            Ingredients = recipe.Ingredients;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Method { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}