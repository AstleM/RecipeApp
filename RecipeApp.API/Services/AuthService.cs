using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RecipeApp.API.Contracts.Services;
using RecipeApp.API.Data;
using RecipeApp.API.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RecipeApp.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApiUser> userManager;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AuthService(UserManager<ApiUser> userManager, IMapper mapper, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<AuthResponseDto> Login(ApiUserLoginDto userDto)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(userDto.Email);

                if (user == null)
                    return null;

                bool validPassword = await userManager.CheckPasswordAsync(user, userDto.Password);

                if (!validPassword)
                    return null;

                string token = await GenerateToken(user);

                return new AuthResponseDto
                {
                    Token = token,
                    UserId = user.Id
                };
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<IdentityError>> Register(ApiUserCreateDto userDto)
        {
            ApiUser user = mapper.Map<ApiUser>(userDto);
            user.UserName = userDto.Email;
            var result = await userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
            }

            return result.Errors;
        }

        private async Task<string> GenerateToken(ApiUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await userManager.GetRolesAsync(user);

            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            var userClaims = await userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
