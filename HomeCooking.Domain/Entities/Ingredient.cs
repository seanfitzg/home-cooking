namespace HomeCooking.Domain.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string FoodType { get;  set; }
        public Weight Weight { get; set; }
        public Volume Volume { get; set; }
        public Other Other { get; set; }
    }
}