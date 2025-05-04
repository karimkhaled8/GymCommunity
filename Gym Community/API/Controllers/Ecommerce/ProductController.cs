using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces;
using Gym_Community.Application.Interfaces.IE_comm;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<IActionResult> Get(
            string query = ""
            , int page = 1
            , int eleNo = 8
            , int? categoryId = null
            , int? brandId = null
            , string sort ="asc"
            , decimal? minPrice = null
            , decimal? maxPrice = null
            )
        {
            var userId = getUserId();
            var userRole = "Client"; 
            //var access="Client"; 
            if (string.IsNullOrEmpty(userId))
            {
                
            }
            var totalCount = await _productService.GetTotalCount(query,categoryId,brandId,minPrice,maxPrice);
            var totalPages = (int)Math.Ceiling((double)totalCount / eleNo);
            var products = await _productService.GetProducts(query, page, eleNo,categoryId,brandId,sort,minPrice,maxPrice);

            return Ok(new
            {
                success = true,
                data = products,
                totalCount = totalCount,
                totalPages = totalPages,
                message = products.Any() ? null : "No products found"
            });
        }




        [HttpGet("user")]
        public async Task<IActionResult> GetUser([FromQuery] string userid)
        {
            if (string.IsNullOrEmpty(userid))
            {
                return BadRequest(new { message = "UserId is required" });
            }
            var products = await _productService.GetUserProducts(userid);
            return products.Any() ? Ok(products) : Ok(new { success = true, message = "No products Found" });
        }

        // filter by category 
        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategory(categoryId);
            return products.Any()
                ? Ok(products)
                : Ok(new { success = true, message = "No products found for this category" });
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
            var userId = getUserId();
            if (userId == null) return Unauthorized(); 
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
            var createdProduct = await _productService.CreateProduct(productDto,userId);
            return createdProduct==null ? NotFound() : Ok(createdProduct);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] IFormFile productImg, [FromForm] string productDTO)
        {
            try
            {
                // Deserialize the product data
                var productDto = JsonConvert.DeserializeObject<ProductDTO>(productDTO);

                if (productDto == null || productDto.Id != id)
                {
                    return BadRequest("Invalid product data");
                }

                // Handle image upload only if a new file was provided
                if (productImg != null && productImg.Length > 0)
                {
                    if (!string.IsNullOrEmpty(productDto.ImageUrl))
                    {
                        await _awsService.DeleteFileAsync(productDto.ImageUrl);
                    }

                    var imageUrl = await _awsService.UploadFileAsync(productImg, "products");
                    if (string.IsNullOrEmpty(imageUrl))
                    {
                        return BadRequest(new { success = false, message = "Failed to upload image" });
                    }
                    productDto.ImageUrl = imageUrl;
                }

                // Validate model after potential image update
                if (!TryValidateModel(productDto))
                {
                    return BadRequest(ModelState);
                }

                var updatedProduct = await _productService.UpdateProduct(id, productDto);
                return updatedProduct == null ? NotFound() : Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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

        // for filteration by brand
        [HttpGet("by-brand/{brandId}")]
        public async Task<IActionResult> GetProductsByBrand(int brandId)
        {
            var products = await _productService.GetProductsByBrand(brandId);
            return products.Any()
                ? Ok(products)
                : Ok(new { success = true, message = "No products found for this brand" });
        }

        // filter by price 

        // In ProductController.cs
        [HttpGet("by-price")]
        public async Task<IActionResult> GetProductsByPriceRange(
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int? categoryId)
        {
            IEnumerable<ProductDTO> products;

            if (categoryId.HasValue)
            {
                products = await _productService.GetProductsByPriceRangeAndCategory(
                    categoryId, minPrice, maxPrice);
            }
            else
            {
                products = await _productService.GetProductsByPriceRange(minPrice, maxPrice);
            }

            return products.Any()
                ? Ok(products)
                : Ok(new { success = true, message = "No products found in this price range" });
        }

    }
}
