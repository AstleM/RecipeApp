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

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = tokenHandler.ReadJwtToken(token);
            var userId = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == "uid").Value;

            recipe.UserId = userId;

            ApiUser user = await userManager.FindByIdAsync(userId);

            recipe.User = user;

            Recipe newRecipe = await recipeRepo.CreateAsync(recipe);

            RecipeGetDetailDto recipeGetDetailDto = mapper.Map<RecipeGetDetailDto>(newRecipe);

            return recipeGetDetailDto;
        }
    }
}
