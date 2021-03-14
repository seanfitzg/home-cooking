using MediatR;

namespace HomeCooking.Application
{
    public class DeleteRecipeCommand : IRequest<bool>
    {
        public int RecipeId { get; set; }

        public DeleteRecipeCommand(int recipeId)
        {
            RecipeId = recipeId;
        }
    }
}