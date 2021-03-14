using System;
using System.Collections.Generic;
using HomeCooking.Domain.Entities;
using MediatR;

namespace HomeCooking.Application
{
    public class CreateRecipeCommand : IRequest<int>
    {
        public CreateRecipeCommand()
        {
            Ingredients = new List<Ingredient>();
        }
        
        public CreateRecipeCommand(string userId, string name, string method, string description, IEnumerable<Ingredient> ingredients)
        {
            UserId = userId;
            Name = name;
            Method = method;
            Description = description;
            Ingredients = ingredients;
        }
        
        public string UserId { get; set; }
        public string Name { get; set;  }
        public string Method { get;  set;}
        public string Description { get;  set;}
        public IEnumerable<Ingredient> Ingredients { get;  set; }
    }
}