using System.Collections.Generic;
using HomeCooking.Domain.Entities;
using MediatR;

namespace HomeCooking.Application
{
    public class UpdateRecipeCommand : IRequest<bool>
    {
        public UpdateRecipeCommand()
        {
            Ingredients = new List<Ingredient>();
        }
        public UpdateRecipeCommand(int recipeId, string userId, string name, string method, string description, IEnumerable<Ingredient> ingredients)
        {
            RecipeId = recipeId;
            UserId = userId;
            Name = name;
            Method = method;
            Description = description;
            Ingredients = ingredients;
        }

        public int RecipeId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set;  }
        public string Method { get; set;  }
        public string Description { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}