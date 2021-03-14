using System.Threading;
using System.Threading.Tasks;
using HomeCooking.Data;
using HomeCooking.Domain.Entities;
using MediatR;

namespace HomeCooking.Application
{
    public class CreateRecipeHandler : IRequestHandler<CreateRecipeCommand, int>
    {
        private readonly IRecipeRepository _recipeRepository;

        public CreateRecipeHandler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        
        public Task<int> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = new Recipe()
            {
                UserId = request.UserId,
                Name = request.Name,
                Description = request.Description,
                Method = request.Method,
                Ingredients = request.Ingredients
            };
                
            var id = _recipeRepository.AddRecipe(recipe);
            return Task.FromResult(id);
        }
    }
}