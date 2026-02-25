using Project.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Interfaces
{
    public interface IProductCategoryService
    {
        Task CreateAsync(ProductCategory category);

        Task<(IEnumerable<ProductCategory> Items, int TotalCount)> GetAllAsync(
            string? search,
            string? sortBy,
            int page,
            int pageSize);

        Task<ProductCategory?> GetByIdAsync(int id);

        Task<bool> UpdateAsync(int id, ProductCategory category);

        Task<bool> DeleteAsync(int id);
    }
}
