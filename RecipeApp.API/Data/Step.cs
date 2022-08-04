using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.API.Data
{
    public class Step
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int StepNumber { get; set; }
        [ForeignKey(nameof(RecipeId))]
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
