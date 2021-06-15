using System.Collections.Generic;
using System.Linq;
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
            Ingredients = recipe.Ingredients.Select(p => new IngredientDto(p)).ToList();
            UserId = recipe.UserId;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Method { get; set; }
        public IList<IngredientDto> Ingredients { get; set; }
        public string UserId { get; set; }
    }
}