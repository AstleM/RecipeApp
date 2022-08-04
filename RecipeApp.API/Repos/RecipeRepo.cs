using RecipeApp.API.Contracts.Repos;
using RecipeApp.API.Data;

namespace RecipeApp.API.Repos
{
    public class RecipeRepo : BaseRepo<Recipe>, IRecipeRepo
    {
        public RecipeRepo(RecipeDbContext context) : base(context)
        {
        }
    }
}
