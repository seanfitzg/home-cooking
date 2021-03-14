using HomeCooking.Domain.Entities;
using MediatR;

namespace HomeCooking.Application
{
    public class GetRecipeCommand : IRequest<Recipe>
    {
        public GetRecipeCommand(int recipeId)
        {
            RecipeId = recipeId;
        }

        public int RecipeId { get; }
    }
}