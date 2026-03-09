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
            try
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
            catch (Exception)
            {
                return Problem("An unexpected error occured while retriving the products");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {


                var product = await _productService.GetByIdAsync(id);

                if (product == null)
                    return NotFound("There is not a product with the given Id");

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
            catch (Exception) 
            {
                return Problem("An unexpected error occured while retriving the product");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            try
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
                return Ok("The product has been created!");
            }
            catch (Exception)
            {
                return Problem("An unexpected error occured while creating the product");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        {
            try
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
                    return NotFound("There is not a product with the given Id");

                return Ok("The product has been updated!");
            }
            catch (Exception)
            {
                return Problem("An unexpected error occured while updating the product");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _productService.DeleteAsync(id);

                if (!success)
                    return NotFound("There is not a product with the given Id");

                return Ok("The product has been deleted!");
            }
            catch (Exception)
            {
                return Problem("An unexpected error occured while deleting the product");
            }
        }
    }
}
