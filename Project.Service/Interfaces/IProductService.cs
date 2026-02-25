using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Service.Entities;

namespace Project.Service.Interfaces
{
    public interface IProductService
    {
        Task<(IEnumerable<Product> Items, int TotalCount)> GetAllAsync(
            int? categoryId,
            decimal? minPrice,
            decimal? maxPrice,
            bool? isActive,
            string? sortBy,
            int page,
            int pageSize);
        Task<Product?> GetByIdAsync(int id);
        Task CreateAsync(Product product);
        Task<bool> UpdateAsync(int id, Product product);
        Task<bool> DeleteAsync(int id);
        
    }
}
