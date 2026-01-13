using LumelSalesManagementRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumelSalesManagementRepository.Interfaces
{
    public interface IProductQueryRepository
    {
        Task<ProductDetails> GetProductByIdAsync(Guid productId);
        Task<List<ProductDetails>> GetAllProductsAsync();
        Task<ProductCategory> GetCategoryByCategoryIdAsync(Guid categoryId);
        Task<List<ProductCategory>> GetAllCategoriesAsync();
    }
}
