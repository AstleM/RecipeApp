using RecipeApp.API.Contracts.Repos;
using RecipeApp.API.Data;

namespace RecipeApp.API.Repos
{
    public class IngredientRepo : BaseRepo<Ingredient>, IIngredientRepo
    {
        public IngredientRepo(RecipeDbContext context) : base(context)
        {
        }
    }
}
