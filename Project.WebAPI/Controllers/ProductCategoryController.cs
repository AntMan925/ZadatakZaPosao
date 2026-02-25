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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            return Ok(new ProductCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCategoryDto dto)
        {
            var category = new ProductCategory
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _service.CreateAsync(category);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductCategoryDto dto)
        {
            var category = new ProductCategory
            {
                Name = dto.Name,
                Description = dto.Description
            };

            var success = await _service.UpdateAsync(id, category);

            if (!success)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);

            if (!success)
                return NotFound();
                
            return Ok();
        }
    }
}
