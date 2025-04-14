using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces.IE_comm;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gym_Community.API.Controllers.Ecommerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        IBrandService _brandService; 
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService; 
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var brands = await _brandService.GetAllBrands(); 
            return Ok(brands);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var brand = await _brandService.GetBrandById(id);
            if (brand == null) return NotFound(); 
            return Ok(brand);
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BrandDTO brandDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); 
            var createdBrand = await _brandService.CreateBrand(brandDTO);
            return CreatedAtAction(nameof(Get),new {id=createdBrand.BrandID , createdBrand}); 

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BrandDTO brandDTO)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);  

            var updatedBrand = await _brandService.UpdateBrand(id, brandDTO);
            
            if(updatedBrand == null) return NotFound();

            return Ok(updatedBrand); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           bool response =  await _brandService.DeleteBrand(id);
            if(!response) return NotFound();

            return Ok(new {success = response});    
        }

    }
}
