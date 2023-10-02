using System.Threading;
using System.Threading.Tasks;
using HomeCooking.Data;
using HomeCooking.Domain.Entities;
using MediatR;

namespace HomeCooking.Application
{
    public class UpdateRecipeHandler : IRequestHandler<UpdateRecipeCommand, bool>
    {
        private readonly IRecipeRepository _recipeRepository;

        public UpdateRecipeHandler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        
        public Task<bool> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = new Recipe()
            {
                Id = request.RecipeId,
                Name = request.Name,
                Description = request.Description,
                Method = request.Method,
                Ingredients = request.Ingredients,
                UserId = request.UserId
            };
            _recipeRepository.UpdateRecipe(recipe);
            return Task.FromResult(true);
        }
    }
}