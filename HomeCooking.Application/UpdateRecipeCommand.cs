using System.Collections.Generic;
using HomeCooking.Application.DTOs;
using HomeCooking.Domain.Entities;
using MediatR;

namespace HomeCooking.Application
{
    public class UpdateRecipeCommand : IRequest<bool>
    {
        public UpdateRecipeCommand(RecipeDto recipe, string userId)
        {
            UserId = userId;
            RecipeId = recipe.Id;
            Name = recipe.Name;
            Method = recipe.Method;
            Description = recipe.Description;
            Ingredients = recipe.Ingredients;
        }

        public int RecipeId { get; }
        public string UserId { get; }
        public string Name { get; }
        public string Method { get; }
        public string Description { get; }
        public IEnumerable<Ingredient> Ingredients { get; }
    }
}