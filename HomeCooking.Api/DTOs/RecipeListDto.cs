namespace HomeCooking.Api.DTOs
{
    public struct RecipeListDto
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        
        public RecipeListDto(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }        
    }
}