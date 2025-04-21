using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.IE_comm;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gym_Community.API.Controllers.Ecommerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IAwsService _awsService;

        public ProductController(IProductService ps,IAwsService aws)
        {
            _productService = ps;
            _awsService = aws; 
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetProducts();
            return products.Any() ? Ok(products) : Ok(new {success = true ,message = "No products Found" });
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            var products = await _productService.GetUserProducts(getUserId());
            return products.Any() ? Ok(products) : Ok(new { success = true, message = "No products Found" });
        }
        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetProductsByCategory(int id)
        {
            var products = await _productService.getProductsByCategory(id);
            return products.Any() ? Ok(products) : Ok(new { success = true, message = "No products Found" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 0) return BadRequest(); 
            var product = await _productService.GetProductById(id); 
            if(product==null) return NotFound();    
            return Ok(product);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            var products = await _productService.SearchProducts(name);
            return products.Any() ? Ok(products) : Ok(new { success = false, message = "No matching products found." });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductDTO productDto, [FromForm] IFormFile productImg)
        {
            if (productImg == null || productImg.Length == 0)
            {
                return BadRequest(new {success=false , message = "Product Image is required"});
            }
            var imageUrl = await _awsService.UploadFileAsync(productImg, "products");
            if (string.IsNullOrEmpty(imageUrl))
            {
                return BadRequest(new { success = false, message = "Failed to upload image" });
            }
            if(!ModelState.IsValid) return BadRequest(ModelState);
            productDto.ImageUrl = imageUrl;
            var createdProduct = await _productService.CreateProduct(productDto);
            return createdProduct==null ? NotFound() : Ok(createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] ProductDTO productDTO , [FromForm] IFormFile productImg)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (productImg != null && productImg.Length > 0)
            {
                if(!string.IsNullOrEmpty(productDTO.ImageUrl)) 
                    await _awsService.DeleteFileAsync(productDTO.ImageUrl);

                var imageUrl = await _awsService.UploadFileAsync(productImg, "products");
                if (string.IsNullOrEmpty(imageUrl))
                {
                   return BadRequest(new { success = false, message = "Failed to upload image" });
                }
                productDTO.ImageUrl = imageUrl;
            }
            var updatedProduct = await _productService.UpdateProduct(id, productDTO);
            if (updatedProduct == null) return NotFound();
            return Ok(updatedProduct); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedProduct = await _productService.DeleteProduct(id);
            if (!deletedProduct) return NotFound();
            return Ok(new { success = true, message = "Product deleted successfully" }); 
        }

        private string getUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
