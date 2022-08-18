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
            CreateMap<RecipeGetDto, Recipe>().ReverseMap();
            CreateMap<RecipeUpdateDto, Recipe>().ReverseMap();
            #endregion

            #region Ingredients
            CreateMap<IngredientCreateDto, Ingredient>().ReverseMap();
            CreateMap<IngredientGetDto, Ingredient>().ReverseMap();
            CreateMap<IngredientUpdateDto, Ingredient>().ReverseMap();
            #endregion

            #region Steps
            CreateMap<StepCreateDto, Step>().ReverseMap();
            CreateMap<StepGetDto, Step>().ReverseMap();
            CreateMap<StepUpdateDto, Step>().ReverseMap();
            #endregion
        }
    }
}
