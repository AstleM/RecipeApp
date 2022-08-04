namespace RecipeApp.API.Dtos
{
    public class RecipeCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<IngredientCreateDto> Ingredients { get; set; }
        public IList<StepCreateDto> Steps { get; set; }
    }
}
