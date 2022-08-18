using Microsoft.EntityFrameworkCore;
using RecipeApp.API.Contracts.Repos;
using RecipeApp.API.Data;

namespace RecipeApp.API.Repos
{
    public class RecipeRepo : BaseRepo<Recipe>, IRecipeRepo
    {
        private readonly RecipeDbContext context;

        public RecipeRepo(RecipeDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IList<Recipe>> GetAllForUserAsync(string userId)
        {
            var recipes = context.Recipes.Where(q => q.UserId == userId).ToList();

            return recipes;
        }

        public async Task<Recipe> GetByIdAsync(int id, string userId)
        {
            var recipe = await context.Recipes.Include(q => q.Ingredients).Include(q => q.Steps).FirstOrDefaultAsync(q => q.UserId == userId && q.Id == id);

            return recipe;
        }

        public async Task<Recipe> UpdateRecipeAsync(Recipe recipe)
        {
            Recipe currentRecipe = await GetByIdAsync(recipe.Id, recipe.UserId);
            foreach (Ingredient ingredient in currentRecipe.Ingredients)
            {
                context.Set<Ingredient>().Remove(ingredient);
            }
            foreach (Step step in currentRecipe.Steps)
            {
                context.Set<Step>().Remove(step);
            }
            await context.SaveChangesAsync();

            currentRecipe = recipe;

            await UpdateAsync(currentRecipe);

            return await GetByIdAsync(currentRecipe.Id, currentRecipe.UserId);
        }
    }
}
