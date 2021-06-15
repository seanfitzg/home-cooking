using System.Collections.Generic;
using System.Linq;
using HomeCooking.Application.DTOs;
using HomeCooking.Domain.Entities;
using MediatR;

namespace HomeCooking.Application
{
    public class UpdateRecipeCommand : IRequest<bool>
    {
        public UpdateRecipeCommand(RecipeDto recipeDto, string userId)
        {
            UserId = userId;
            RecipeId = recipeDto.Id;
            Name = recipeDto.Name;
            Method = recipeDto.Method;
            Description = recipeDto.Description;
            Ingredients = recipeDto.Ingredients.Select(IngredientDto.CreateIngredientFromDto).ToList();
        }

        public int RecipeId { get; }
        public string UserId { get; }
        public string Name { get; }
        public string Method { get; }
        public string Description { get; }
        public IList<Ingredient> Ingredients { get; }
    }
}