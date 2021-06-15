using System;
using HomeCooking.Domain.Entities;

namespace HomeCooking.Application.DTOs
{
    public class IngredientDto
    {
        public IngredientDto()
        {
            
        }
        
        public IngredientDto(Ingredient ingredient)
        {
            Id = ingredient.Id;
            Item = ingredient.Item;
            Amount = ingredient.Amount;
        }
        public Guid Id { get; set; }
        public string Item { get;  set; }
        public double Amount { get; set; }
        public bool IsNew { get; set; }
        
        public static Ingredient CreateIngredientFromDto(IngredientDto ingredientDto)
        {
            return new()
            {
                Id = ingredientDto.IsNew ? Guid.Empty : ingredientDto.Id,
                Amount = ingredientDto.Amount,
                Item = ingredientDto.Item
            };
        }
        
    }
}