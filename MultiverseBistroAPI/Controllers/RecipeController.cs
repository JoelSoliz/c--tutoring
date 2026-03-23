using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiverseBistroAPI.DTOs;
using MultiverseBistroAPI.Interfaces.Services;
using System.Security.Claims;

namespace MultiverseBistroAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : Controller
    {
        private readonly IRecipeService _service;

        public RecipeController(IRecipeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll(int page = 1, int limit = 5)
        {
            var recipes = _service.GetRecipes(limit, page);
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var recipe = _service.GetRecipe(id);
                return Ok(recipe);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(RecipeCreateDTO data)
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            var recipe = _service.CreateRecipe(data, userEmail);
            return Ok(recipe);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var result = _service.DeleteRecipe(id);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{id}/image")]
        [Authorize]
        public IActionResult UploadImage(Guid id, IFormFile file)
        {
            try
            {
                var result = _service.UploadImage(id, file);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
