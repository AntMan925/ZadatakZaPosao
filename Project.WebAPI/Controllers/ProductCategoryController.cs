using Microsoft.AspNetCore.Mvc;
using Project.Service.Entities;
using Project.Service.Interfaces;
using Project.Service.Services;
using Project.WebAPI.DTOs;

namespace Project.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _service;

        public ProductCategoryController(IProductCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            string? search,
            string? sortBy,
            int page = 1,
            int pageSize = 10)
        {
            try
            {
                var (items, totalCount) = await _service.GetAllAsync(
                search,
                sortBy,
                page,
                pageSize);

                var result = items.Select(c => new ProductCategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
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
                var category = await _service.GetByIdAsync(id);

                if (category == null)
                    return NotFound("There is not a category with the given Id");

                return Ok(new ProductCategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                });
            }
            catch (Exception)
            {
                return Problem("An unexpected error occured while retriving the product");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCategoryDto dto)
        {
            try
            {
                var category = new ProductCategory
                {
                    Name = dto.Name,
                    Description = dto.Description
                };

                await _service.CreateAsync(category);

                return Ok("The Category has been created!");
            }
            catch (Exception)
            {
                return Problem("An unexpected error occured while creating the product");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductCategoryDto dto)
        {
            try
            {
                var category = new ProductCategory
                {
                    Name = dto.Name,
                    Description = dto.Description
                };

                var success = await _service.UpdateAsync(id, category);

                if (!success)
                    return NotFound("There is not a category with the given Id");

                return Ok("The Category has been updated!");
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
                var success = await _service.DeleteAsync(id);

                if (!success)
                    return NotFound("There is not a category with the given Id");

                return Ok("The Category has been deleted!");
            }
            catch (Exception)
            {
                return Problem("An unexpected error occured while deleting the product");
            }
        }
    }
}
