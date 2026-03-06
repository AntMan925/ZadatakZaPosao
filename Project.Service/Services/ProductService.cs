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
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Product> Items, int TotalCount)> GetAllAsync(
            int? categoryId,
            decimal? minPrice,
            decimal? maxPrice,
            bool? isActive,
            string? sortBy,
            int page,
            int pageSize)
        {
            var query = _context.Products.AsNoTracking().Include(x => x.Category).AsQueryable();

            // FILTERING
            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (isActive.HasValue)
                query = query.Where(p => p.IsActive == isActive.Value);

            // SORTING
            query = sortBy?.ToLower() switch
            {
                "price" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                "name" => query.OrderBy(p => p.Name),
                "name_desc" => query.OrderByDescending(p => p.Name),
                "createdat" => query.OrderBy(p => p.CreatedAt),
                "createdat_desc" => query.OrderByDescending(p => p.CreatedAt),
                _ => query.OrderBy(p => p.Id)
                
            };

            var totalCount = await query.CountAsync();

            // PAGING 
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (items == null || !items.Any())
                throw new Exception("No elements in the list");

            return (items, totalCount);
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            var test = _context.Products.AsNoTracking().Include(x => x.Category).SingleOrDefaultAsync(y => y.Id == id);
            if (test == null)
                throw new Exception("ID ne postoji");

            //if (_context.Products.Any(x => x.Id == id))
            //    throw new Exception("Id ne postoji");

            return test;
        }

        public Task CreateAsync(Product product)
        {
            _context.Products.Add(product);
            return _context.SaveChangesAsync();
        }
                
        public async Task<bool> UpdateAsync(int id, Product updatedProduct)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing == null)
                return false;

            existing.Name = updatedProduct.Name;
            existing.Price = updatedProduct.Price;
            existing.Stock = updatedProduct.Stock;
            existing.IsActive = updatedProduct.IsActive;
            existing.CategoryId = updatedProduct.CategoryId;
            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
               
    }
}
