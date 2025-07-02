using E_Commerce_Project.Interfaces;
using E_Commerce_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // POST: api/category
        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            await _categoryService.Add(category);
            return Ok("Category added successfully.");
        }

        // DELETE: api/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(id);
            return Ok("Category deleted successfully.");
        }

        // GET: api/category
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }

        // PUT: api/category
        [HttpPut]
        public async Task<IActionResult> Update(Category category)
        {
            await _categoryService.Update(category);
            return Ok("Category updated successfully.");
        }
    }
}
