using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HomeCooking.Data;
using HomeCooking.Domain.Entities;
using MediatR;

namespace HomeCooking.Application
{
    public class GetAllRecipesHandler : IRequestHandler<GetAllRecipesCommand, IEnumerable<Recipe>>
    {
        private readonly IRecipeRepository _recipeRepository;

        public GetAllRecipesHandler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        
        public Task<IEnumerable<Recipe>> Handle(GetAllRecipesCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_recipeRepository.GetAllRecipes(request.UserId));
        }
    }
}