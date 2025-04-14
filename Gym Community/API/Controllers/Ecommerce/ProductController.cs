using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.IE_comm;
using Microsoft.AspNetCore.Mvc;

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
        //[HttpGet]
        //public Task<IActionResult> Get()
        //{
        //    var products = _p
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductDTO productDto, [FromForm] IFormFile productImg)
        {
            // Validate the product image
            if (productImg == null || productImg.Length == 0)
            {
                return BadRequest(new {success=false , message = "Product Image is required"});
            }
            var imageUrl = await _awsService.UploadFileAsync(productImg, "products");
            if (string.IsNullOrEmpty(imageUrl))
            {
                return BadRequest(new { success = false, message = "Failed to upload image" });
            }
            productDto.ImageUrl = imageUrl;
            var createdProduct = await _productService.CreateProduct(productDto);
            return createdProduct==null ? NotFound() : Ok(createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] ProductDTO productDTO , [FromForm] IFormFile productImg)
        {
            if (productImg != null && productImg.Length > 0)
            {
                var imageUrl = await _awsService.UploadFileAsync(productImg, "products");
                if (string.IsNullOrEmpty(imageUrl))
                {
                    BadRequest(new { success = false, message = "Failed to upload image" });
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
            return Ok(new { sucess = true, message = "Product deleted successfully" }); 
        }
    }
}
