using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipesAPI.Services;
using MongoDB.Driver;
using RecipesAPI.Models;

namespace RecipesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RecipeService _recipeService;

        public RecipesController(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<List<Recipes>> Get()
        {
            return await _recipeService.GetAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Recipes recipes) 
        {
            await _recipeService.CreateAsync(recipes);
            return CreatedAtAction(nameof(Get), new { id = recipes.Id}, recipes);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Add(string id, [FromBody] string recipeId) 
        {
            await _recipeService.AddtoRecipesAsync(id, recipeId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) 
        {
            await _recipeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
