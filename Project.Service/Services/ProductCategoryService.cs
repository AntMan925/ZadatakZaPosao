using Microsoft.EntityFrameworkCore;
using Project.Service.Data;
using Project.Service.Entities;
using Project.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ApplicationDbContext _context;

        public ProductCategoryService(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public Task CreateAsync(ProductCategory category)
        {
            _context.ProductCategories.Add(category);
             return _context.SaveChangesAsync();
        }
        
        public async Task<(IEnumerable<ProductCategory> Items, int TotalCount)> GetAllAsync(
            string? search,
            string? sortBy,
            int page,
            int pageSize)
        {
            var query = _context.ProductCategories.AsNoTracking().Include(x => x.Products).AsQueryable();

            
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(c => c.Name.Contains(search));

            
            query = sortBy?.ToLower() switch
            {
                "name" => query.OrderBy(c => c.Name),
                "name_desc" => query.OrderByDescending(c => c.Name),
                _ => query.OrderBy(c => c.Id)
            };

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public Task<ProductCategory?> GetByIdAsync(int id)
        {
            return _context.ProductCategories.AsNoTracking().Include(x => x.Products).SingleOrDefaultAsync(y => y.Id == id);
        }

        public async Task<bool> UpdateAsync(int id, ProductCategory updated)
        {
            var existing = await _context.ProductCategories.FindAsync(id);
            if (existing == null)
                return false;

            existing.Name = updated.Name;
            existing.Description = updated.Description;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.ProductCategories.FindAsync(id);

            if (category == null)
                return false;

            _context.ProductCategories.Remove(category);
            await _context.SaveChangesAsync();
            return true;

        }

        
    }
}
