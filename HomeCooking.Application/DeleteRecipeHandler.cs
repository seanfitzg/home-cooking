using System.Threading;
using System.Threading.Tasks;
using HomeCooking.Data;
using MediatR;

namespace HomeCooking.Application
{
    public class DeleteRecipeHandler : IRequestHandler<DeleteRecipeCommand, bool>
    {
        private readonly IRecipeRepository _recipeRepository;

        public DeleteRecipeHandler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        
        public Task<bool> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = _recipeRepository.GetById(request.RecipeId);
            _recipeRepository.Delete(recipe);
            return Task.FromResult(true);
        }
    }
}