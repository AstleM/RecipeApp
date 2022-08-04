using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.API.Contracts.Services;
using RecipeApp.API.Dtos;

namespace RecipeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(ApiUserLoginDto userDto)
        {
            AuthResponseDto response = await authService.Login(userDto);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(ApiUserCreateDto userDto)
        {
            var errors = await authService.Register(userDto);

            if (errors.Any())
            {
                foreach(var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                    return BadRequest(ModelState);
                }
            }

            return Ok();
        }
    }
}
