using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.API.Dtos;
using Microsoft.AspNetCore.Identity;
using RecipeApp.API.Contracts.Services;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace RecipeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService recipeService;
        private readonly ILogger<RecipeController> logger;

        public RecipeController(IRecipeService recipeService, ILogger<RecipeController> logger)
        {
            this.recipeService = recipeService;
            this.logger = logger;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<RecipeGetDetailDto>> Create(RecipeCreateDto recipeCreateDto)
        {
            try
            {
                var headers = Request.Headers;

                
                if (headers.ContainsKey("Authorization"))
                {
                    var token = headers["Authorization"];

                    var tokenValue = AuthenticationHeaderValue.Parse(token.First()).Parameter;

                    var recipe = await recipeService.Create(recipeCreateDto, tokenValue);

                    return Ok(recipe);
                }
                else
                {
                    return BadRequest("Token Value not found");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                return BadRequest();
            }
            
        }
    }
}
