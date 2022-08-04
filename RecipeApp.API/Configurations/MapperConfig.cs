using AutoMapper;
using RecipeApp.API.Data;
using RecipeApp.API.Dtos;

namespace RecipeApp.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ApiUserCreateDto, ApiUser>().ReverseMap();
        }
    }
}
