using System.Collections.Generic;
using HomeCooking.Domain.Entities;
using MediatR;

namespace HomeCooking.Application
{
    public class GetAllRecipesCommand : IRequest<IEnumerable<Recipe>>
    {
        public string UserId { get; }

        public GetAllRecipesCommand(string userId)
        {
            UserId = userId;
        }
    }
}