using E_Commerce_Project.Interfaces;
using E_Commerce_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // POST: api/product
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Product product)
        {
            await _productService.Add(product);
            return Ok("Product added successfully.");
        }

        // DELETE: api/product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);
            return Ok("Product deleted successfully.");
        }

        // GET: api/product
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        // GET: api/product/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }

        // PUT: api/product
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Product product)
        {
            await _productService.Update(product);
            return Ok("Product updated successfully.");
        }
    }
}
