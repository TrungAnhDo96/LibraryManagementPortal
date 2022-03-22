using LibraryManagementPortal.DTO;
using LibraryManagementPortal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementPortal.Controllers
{
    [Authorize(Roles = "Super")]
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetAllCategories()
        {
            return await _service.GetAllCategoriesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            return await _service.GetCategoryAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDTO category)
        {
            return await _service.AddCategoryAsync(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO category)
        {
            return await _service.UpdateCategoryAsync(id, category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return await _service.DeleteCategoryAsync(id);
        }

    }
}