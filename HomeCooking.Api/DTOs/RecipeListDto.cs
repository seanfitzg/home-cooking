namespace HomeCooking.Api.DTOs
{
    public struct RecipeListDto
    {
        public int Id { get; }
        public string Name { get; }
        
        public RecipeListDto(int id, string name)
        {
            Id = id;
            Name = name;
        }        
    }
}