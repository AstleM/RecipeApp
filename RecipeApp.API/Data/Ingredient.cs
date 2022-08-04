using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.API.Data
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(RecipeId))]
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
