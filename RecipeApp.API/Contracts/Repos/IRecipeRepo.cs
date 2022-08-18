using RecipeApp.API.Data;

namespace RecipeApp.API.Contracts.Repos
{
    public interface IRecipeRepo : IBaseRepo<Recipe>
    {
        Task<IList<Recipe>> GetAllForUserAsync(string userId);
        Task<Recipe> GetByIdAsync(int id, string userId);
        Task<Recipe> UpdateRecipeAsync(Recipe recipe);
    }
}
