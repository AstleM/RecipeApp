using AutoMapper;
using RecipeApp.API.Data;
using RecipeApp.API.Dtos;

namespace RecipeApp.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region Users
            CreateMap<ApiUserCreateDto, ApiUser>().ReverseMap();
            #endregion

            #region Recipes
            CreateMap<RecipeCreateDto, Recipe>().ReverseMap();
            CreateMap<RecipeGetDetailDto, Recipe>().ReverseMap();
            #endregion

            #region Ingredients
            CreateMap<IngredientCreateDto, Ingredient>().ReverseMap();
            CreateMap<IngredientGetDto, Ingredient>().ReverseMap();
            #endregion

            #region Steps
            CreateMap<StepCreateDto, Step>().ReverseMap();
            CreateMap<StepGetDto, Step>().ReverseMap();
            #endregion
        }
    }
}
