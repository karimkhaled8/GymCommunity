using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Infrastructure.Interfaces.ECommerce;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gym_Community.API.Controllers.Ecommerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService; 
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null) return NotFound(); 
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            var CreatedCategory = await _categoryService.CreateCategory(categoryDTO.Name); 
            if (CreatedCategory == null) return BadRequest(new {success = false, message = "Category not created" });
            return CreatedAtAction(nameof(Get), new { id = CreatedCategory.CategoryID }, CreatedCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDTO categoryDTO)
        {
            var category = await _categoryService.UpdateCategory(id, categoryDTO);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            if (!result) return NotFound();
            return Ok(new { success = true, message = "Category deleted successfully" });
        }
    }
}
