namespace RecipeApp.API.Dtos
{
    public class RecipeUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<IngredientUpdateDto> Ingredients { get; set; }
        public IList<StepUpdateDto> Steps { get; set; }
    }
}
