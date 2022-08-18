using RecipeApp.API.Dtos;

namespace RecipeApp.API.Contracts.Services
{
    public interface IRecipeService
    {
        Task<RecipeGetDetailDto> Create(RecipeCreateDto recipeCreateDto, string token);
        Task<IList<RecipeGetDto>> GetAll(string token);
        Task<RecipeGetDetailDto> Get(int id, string token);
        Task<RecipeGetDetailDto> Update(RecipeUpdateDto recipeUpdateDto, int id, string token);
    }
}
