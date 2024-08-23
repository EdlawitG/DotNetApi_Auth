using authApi.Dtos;
using authApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace authApi.Controllers
{
    [ApiController]
    [Route("product/[controller]")]
    public class AuthController(ProductService service) : Controller
    {
        private readonly ProductService _service = service;
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var products = await _service.GetProductsAsync();

                return Ok(new
                {
                    message = "Successfully retrieved all products",
                    products = products
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving all products",
                    error = ex.Message
                });
            }
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var product1 = await _service.GetProductAsync(id);
                return Ok(new { message = "Successfully retrieved Product", product = product1 });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the  crating Todo Item", error = ex.Message });

            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProducts([FromForm] CreateProRequest request)
        {
            try
            {
                var product = await _service.CreateProductAsync(request);
                return Ok(new
                {
                    message = "Product created successfully",
                    product
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while creating the product.",
                    error = ex.Message
                });
            }
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var pro = await _service.GetProductAsync(id);
                if (pro == null)
                {
                    return NotFound(new { message = $"No Todo item with Id {id} found." });
                }
                await _service.UpdateProductAsync(id, request);
                return Ok(new { message = $" Product Item  with id {id} successfully updated" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while updating the Todo item with", error = ex.Message });
            }
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var product = await _service.GetProductAsync(id);
                if (product == null)
                {
                    return NotFound(new { message = $"No product with Id {id} found." });
                }
                await _service.DeleteProductAsync(id);
                return Ok(new { message = $"Product with id {id} successfully deleted" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while deleting the product", error = ex.Message });
            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchProduct([FromQuery] string searchTerm)
        {
            var product = await _service.SearchProductAsync(searchTerm);
            return Ok(new { Product = product });

        }
        [HttpGet("filter")]
        public async Task<IActionResult> FilterbyCategory([FromQuery] string category)
        {
            var product = await _service.FilterbyCategoryAsync(category);
            return Ok(new { Product = product });
        }
    }

}