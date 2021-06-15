using System.Threading;
using System.Threading.Tasks;
using HomeCooking.Data;
using HomeCooking.Domain.Entities;
using MediatR;

namespace HomeCooking.Application
{
    public class GetRecipeHandler : IRequestHandler<GetRecipeCommand, Recipe>
    {
        private readonly IRecipeRepository _recipeRepository;

        public GetRecipeHandler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        
        public Task<Recipe> Handle(GetRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = _recipeRepository.GetById(request.RecipeId);
            return Task.FromResult(recipe);
        }
    }
}