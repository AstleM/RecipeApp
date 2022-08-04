using RecipeApp.API.Dtos;

namespace RecipeApp.API.Contracts.Services
{
    public interface IRecipeService
    {
        Task<RecipeGetDetailDto> Create(RecipeCreateDto recipeCreateDto, string token);
    }
}
