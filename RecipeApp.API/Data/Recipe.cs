using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.API.Data
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Ingredient> Ingredients { get; set; }
        public IList<Step> Steps { get; set; }
        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        public ApiUser User { get; set; }
    }
}
