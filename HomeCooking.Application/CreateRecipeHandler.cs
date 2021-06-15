using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HomeCooking.Application.EventBus;
using HomeCooking.Data;
using HomeCooking.Domain.Entities;
using HomeCooking.Domain.Events;
using MediatR;

namespace HomeCooking.Application
{
    public class CreateRecipeHandler : IRequestHandler<CreateRecipeCommand, int>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IEventBus _eventBus;

        public CreateRecipeHandler(IRecipeRepository recipeRepository, IEventBus eventBus )
        {
            _recipeRepository = recipeRepository;
            _eventBus = eventBus;
        }
        
        public async Task<int> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
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
            await _eventBus.Send("newrecipe", new RecipeCreated(id, request.Name));
            return id;
        }
    }
}