using Microsoft.AspNetCore.Mvc;
using Project.Service.Entities;
using Project.Service.Interfaces;
using Project.WebAPI.DTOs;
using System.Linq;

namespace Project.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            int? categoryId,
            decimal? minPrice,
            decimal? maxPrice,
            bool? isActive,
            string? sortBy,
            int page = 1,
            int pageSize = 10)
        {
            var (items, totalCount) = await _productService.GetAllAsync(
                categoryId,
                minPrice,
                maxPrice,
                isActive,
                sortBy,
                page,
                pageSize);

            var result = items.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                IsActive = p.IsActive,
                CategoryId = p.CategoryId,
                CreatedAt = p.CreatedAt
            });

            return Ok(new
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Data = result
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                IsActive = product.IsActive,
                CategoryId = product.CategoryId,
                CreatedAt = product.CreatedAt
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                IsActive = dto.IsActive,
                CategoryId = dto.CategoryId
            };

            await _productService.CreateAsync(product);
            return Ok();
        } //trycatch napraviti

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                IsActive = dto.IsActive,
                CategoryId = dto.CategoryId
            };

            var success = await _productService.UpdateAsync(id, product);

            if (!success)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _productService.DeleteAsync(id);

            if (!success)
                return NotFound();

            return Ok();
        }
    }
}
