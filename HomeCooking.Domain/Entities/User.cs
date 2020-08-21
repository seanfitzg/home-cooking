using System.Collections.Generic;

namespace HomeCooking.Domain.Entities
{
    public class User
    {
        public int Id { get;  set; }
        public string Name { get; set; }
        public IEnumerable<Recipe> Recipes { get; set; }
    }
}