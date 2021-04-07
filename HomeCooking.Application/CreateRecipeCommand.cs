using System;
using System.Collections.Generic;
using HomeCooking.Application.DTOs;
using HomeCooking.Domain.Entities;
using MediatR;

namespace HomeCooking.Application
{
    public class CreateRecipeCommand : IRequest<int>
    {
        public CreateRecipeCommand(RecipeDto recipe, string userId)
        {
            UserId = userId;
            Name = recipe.Name;
            Method = recipe.Method;
            Description = recipe.Description;
            Ingredients = recipe.Ingredients;
        }

        public string UserId { get; set; }
        public string Name { get; set;  }
        public string Method { get;  set;}
        public string Description { get;  set;}
        public IEnumerable<Ingredient> Ingredients { get;  set; }
    }
}