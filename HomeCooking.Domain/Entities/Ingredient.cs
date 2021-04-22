namespace HomeCooking.Domain.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string FoodItem { get;  set; }
        public double Quantity { get; set; }
        public string QuantityType { get; set; }
    }
}