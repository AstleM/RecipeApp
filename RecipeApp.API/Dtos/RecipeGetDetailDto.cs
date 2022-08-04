namespace RecipeApp.API.Dtos
{
    public class RecipeGetDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<IngredientGetDto> Ingredients { get; set; }
        public IList<StepGetDto> Steps { get; set; }
    }
}
