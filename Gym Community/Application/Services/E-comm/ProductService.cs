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

        public async Task<IEnumerable<ProductDTO>> GetProducts(string query, int page, int eleNo, int? categoryId, int? brandId, string sort, decimal? minPrice, decimal? maxPrice)
        {
            var products = await _productRepository.ListAsync(query,page,eleNo, sort, categoryId,brandId,minPrice,maxPrice);
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
                BrandName = p.Brand?.Name ?? string.Empty

            });

            return productDtos;
        }
        public async Task<int> GetTotalCount(string query,int? categoryId,int? brandId,decimal? minPrice,decimal? maxPrice)
        {
            return await _productRepository.GetTotalCount(query,categoryId,brandId,minPrice,maxPrice); 
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
                BrandName = p.Brand?.Name ?? string.Empty

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
                BrandName = product.Brand?.Name ?? string.Empty

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
                BrandName = addedProduct.Brand?.Name ?? string.Empty
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
                BrandName = updatedProduct.Brand?.Name ?? string.Empty
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
                BrandName = p.Brand?.Name ?? string.Empty

            });
            return productDtos;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByBrand(int brandId)
        {
            var products = await _productRepository.ListAsync(); // Gets all products with includes

            return products
                .Where(p => p.BrandId == brandId)
                .Select(p => new ProductDTO
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
                    BrandName = p.Brand?.Name ?? string.Empty // Fixed: Changed from p.Category to p.Brand
                })
                .ToList();
        }

        // filter by category 

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryAsync(categoryId);
            return products.Select(p => new ProductDTO
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
                BrandName = p.Brand?.Name ?? string.Empty

            });
        }

        // filter by price 
        public async Task<IEnumerable<ProductDTO>> GetProductsByPriceRange(decimal? minPrice, decimal? maxPrice)
        {
            var products = await _productRepository.GetProductsByPriceRangeAsync(minPrice, maxPrice);
            return MapProductsToDTOs(products);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByPriceRangeAndCategory(int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            var products = await _productRepository.GetProductsByPriceRangeAndCategoryAsync(categoryId, minPrice, maxPrice);
            return MapProductsToDTOs(products);
        }

        private IEnumerable<ProductDTO> MapProductsToDTOs(IEnumerable<Product> products)
        {
            return products.Select(p => new ProductDTO
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
                BrandName = p.Brand?.Name ?? string.Empty
            });
        }
    }
}