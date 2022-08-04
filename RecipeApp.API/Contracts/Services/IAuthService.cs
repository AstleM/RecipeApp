using Microsoft.AspNetCore.Identity;
using RecipeApp.API.Dtos;

namespace RecipeApp.API.Contracts.Services
{
    public interface IAuthService
    {
        Task<IEnumerable<IdentityError>> Register(ApiUserCreateDto userDto);
        Task<AuthResponseDto> Login(ApiUserLoginDto userDto);
    }
}
