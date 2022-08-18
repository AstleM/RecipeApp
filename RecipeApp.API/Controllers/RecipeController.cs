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
                string token = GetToken();


                if (token != null)
                {
                    var recipe = await recipeService.Create(recipeCreateDto, token);

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

        [HttpGet]
        public async Task<ActionResult<RecipeGetDto>> Get()
        {
            try
            {
                string token = GetToken();
                if(token != null)
                {
                    var recipes = await recipeService.GetAll(token);
                    return Ok(recipes);
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

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<RecipeGetDetailDto>> Get(int id)
        {
            try
            {
                string token = GetToken();
                if (token != null)
                {
                    var recipe = await recipeService.Get(id, token);
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

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<RecipeGetDetailDto>> Update(RecipeUpdateDto recipeUpdateDto, int id)
        {
            try
            {
                string token = GetToken();
                if (token != null)
                {
                    var recipe = await recipeService.Update(recipeUpdateDto, id, token);
                    return Ok(recipe);
                }
                else
                {
                    return BadRequest("Token Value not found");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message + ex.StackTrace);

                return BadRequest();
            }
        }

        private string GetToken()
        {
            var headers = Request.Headers;

            if (headers.ContainsKey("Authorization"))
            {
                var token = headers["Authorization"];

                var tokenValue = AuthenticationHeaderValue.Parse(token.First()).Parameter;

                return tokenValue;
            }
            else
            {
                return null;
            }
        }
    }
}
