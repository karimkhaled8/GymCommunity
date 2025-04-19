using Gym_Community.API.DTOs.E_comm;
using Gym_Community.Application.Interfaces.IE_comm;
using Gym_Community.Domain.Data.Models.E_comms;
using Gym_Community.Infrastructure.Interfaces.ECommerce;

namespace Gym_Community.Application.Services.E_comm
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = await _productRepository.ListAsync();
            var productDtos = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl,
                CreatedAt = p.CreatedAt,
                AverageRating = p.AverageRating,
                CategoryID = p.CategoryID,
                CategoryName = p.Category?.Name ?? string.Empty,
                BrandId = p.BrandId,
                BrandName = p.Category?.Name ?? string.Empty
            });

            return productDtos;
        }
        public async Task<IEnumerable<ProductDTO>> SearchProducts(string name)
        {
            var products = await _productRepository.ListAsync(name);
            var productDtos = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl,
                CreatedAt = p.CreatedAt,
                AverageRating = p.AverageRating,
                CategoryID = p.CategoryID,
                CategoryName = p.Category?.Name ?? string.Empty,
                BrandId = p.BrandId,
                BrandName = p.Category?.Name ?? string.Empty
            });

            return productDtos;
        }

        public async Task<ProductDTO?> GetProductById(int productId)
        {
            var product = await _productRepository.GetById(productId);
            if (product == null) return null;

            var productDto = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                ImageUrl = product.ImageUrl,
                CreatedAt = product.CreatedAt,
                AverageRating = product.AverageRating,
                CategoryID = product.CategoryID,
                CategoryName = product.Category?.Name ?? string.Empty,
                BrandId = product.BrandId,
                BrandName = product.Category?.Name ?? string.Empty
            };

            return productDto;
        }

        public async Task<ProductDTO?> CreateProduct(ProductDTO productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                ImageUrl = productDto.ImageUrl,
                CreatedAt = DateTime.Now,
                AverageRating = productDto.AverageRating,
                CategoryID = productDto.CategoryID,
                BrandId = productDto.BrandId
            };

            var addedProduct = await _productRepository.AddAsync(product);

            return addedProduct == null ? null : new ProductDTO
            {
                Id = addedProduct.Id,
                Name = addedProduct.Name,
                Description = addedProduct.Description,
                Price = addedProduct.Price,
                Stock = addedProduct.Stock,
                ImageUrl = addedProduct.ImageUrl,
                CreatedAt = addedProduct.CreatedAt,
                AverageRating = addedProduct.AverageRating,
                CategoryID = addedProduct.CategoryID,
                CategoryName = addedProduct.Category?.Name ?? string.Empty,
                BrandId = addedProduct.BrandId,
                BrandName = addedProduct.Category?.Name ?? string.Empty
            }; 
        }

        public async Task<ProductDTO?> UpdateProduct(int productId, ProductDTO productDto)
        {
            var existingProduct = await _productRepository.GetById(productId);
            if (existingProduct == null) return null;

            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.Price = productDto.Price;
            existingProduct.Stock = productDto.Stock;
            existingProduct.ImageUrl = productDto.ImageUrl;
            existingProduct.AverageRating = productDto.AverageRating;
            existingProduct.CategoryID = productDto.CategoryID;
            existingProduct.BrandId = productDto.BrandId;   


            var updatedProduct = await _productRepository.UpdateAsync(existingProduct);
            return updatedProduct==null ? null : new ProductDTO {
                Id = updatedProduct.Id,
                Name = updatedProduct.Name,
                Description = updatedProduct.Description,
                Price = updatedProduct.Price,
                Stock = updatedProduct.Stock,
                ImageUrl = updatedProduct.ImageUrl,
                CreatedAt = updatedProduct.CreatedAt,
                AverageRating = updatedProduct.AverageRating,
                CategoryID = updatedProduct.CategoryID,
                CategoryName = updatedProduct.Category?.Name ?? string.Empty,
                BrandId = updatedProduct.BrandId,
                BrandName = updatedProduct.Category?.Name ?? string.Empty
            };

        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _productRepository.GetById(productId);
            if (product == null) return false;

            await _productRepository.RemoveAsync(product);
            return true;
        }

        public async Task<IEnumerable<ProductDTO>> GetUserProducts(string userId)
        {
            var proudcts = await _productRepository.ListUserAsync(userId);
            var productDtos = proudcts.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                ImageUrl = p.ImageUrl,
                CreatedAt = p.CreatedAt,
                AverageRating = p.AverageRating,
                CategoryID = p.CategoryID,
                CategoryName = p.Category?.Name ?? string.Empty,
                BrandId = p.BrandId,
                BrandName = p.Category?.Name ?? string.Empty
            });
            return productDtos;
        }
    }
}