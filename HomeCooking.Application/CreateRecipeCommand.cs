using System;
using System.Collections.Generic;
using System.Linq;
using HomeCooking.Application.DTOs;
using HomeCooking.Domain.Entities;
using MediatR;

namespace HomeCooking.Application
{
    public class CreateRecipeCommand : IRequest<int>
    {
        public CreateRecipeCommand(RecipeDto recipeDto, string userId)
        {
            UserId = userId;
            Name = recipeDto.Name;
            Method = recipeDto.Method;
            Description = recipeDto.Description;
            Ingredients = recipeDto.Ingredients.Select(IngredientDto.CreateIngredientFromDto).ToList();
        }

        public string UserId { get; set; }
        public string Name { get; set;  }
        public string Method { get;  set;}
        public string Description { get;  set;}
        public IList<Ingredient> Ingredients { get;  set; }
    }
}