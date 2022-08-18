using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RecipeApp.API.Contracts.Repos;
using RecipeApp.API.Contracts.Services;
using RecipeApp.API.Data;
using RecipeApp.API.Dtos;
using System.IdentityModel.Tokens.Jwt;

namespace RecipeApp.API.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepo recipeRepo;
        private readonly IMapper mapper;
        private readonly UserManager<ApiUser> userManager;

        public RecipeService(IRecipeRepo recipeRepo, IMapper mapper, UserManager<ApiUser> userManager)
        {
            this.recipeRepo = recipeRepo;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<RecipeGetDetailDto> Create(RecipeCreateDto recipeCreateDto, string token)
        {
            Recipe recipe = mapper.Map<Recipe>(recipeCreateDto);

            var userId = getUserIdFromToken(token);

            recipe.UserId = userId;

            ApiUser user = await userManager.FindByIdAsync(userId);

            recipe.User = user;

            Recipe newRecipe = await recipeRepo.CreateAsync(recipe);

            RecipeGetDetailDto recipeGetDetailDto = mapper.Map<RecipeGetDetailDto>(newRecipe);

            return recipeGetDetailDto;
        }

        public async Task Delete(int id)
        {
            await recipeRepo.DeleteAsync(id);
        }

        public async Task<RecipeGetDetailDto> Get(int id, string token)
        {
            string userId = getUserIdFromToken(token);

            Recipe recipe = await recipeRepo.GetByIdAsync(id, userId);

            RecipeGetDetailDto recipeGetDetailDto = mapper.Map<RecipeGetDetailDto>(recipe);

            return recipeGetDetailDto;
        }

        public async Task<IList<RecipeGetDto>> GetAll(string token)
        {
            string userId = getUserIdFromToken(token);

            var recipes = await recipeRepo.GetAllForUserAsync(userId);

            var recipeDtos = mapper.Map<List<RecipeGetDto>>(recipes);

            return recipeDtos;
        }

        public async Task<RecipeGetDetailDto> Update(RecipeUpdateDto recipeUpdateDto, int id, string token)
        {
            Recipe recipe = mapper.Map<Recipe>(recipeUpdateDto);

            recipe.Id = id;

            var userId = getUserIdFromToken(token);

            recipe.UserId = userId;

            ApiUser user = await userManager.FindByIdAsync(userId);

            recipe.User = user;

            Recipe updatedRecipe = await recipeRepo.UpdateRecipeAsync(recipe);

            RecipeGetDetailDto recipeGetDetailDto = mapper.Map<RecipeGetDetailDto>(updatedRecipe);

            return recipeGetDetailDto;
        }

        private string getUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = tokenHandler.ReadJwtToken(token);
            var userId = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == "uid").Value;

            return userId;
        }
    }
}
