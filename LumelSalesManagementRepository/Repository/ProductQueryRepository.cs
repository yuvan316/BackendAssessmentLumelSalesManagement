using LumelSalesManagementRepository.Context;
using LumelSalesManagementRepository.Interfaces;
using LumelSalesManagementRepository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumelSalesManagementRepository.Repository
{
    public class ProductQueryRepository : IProductQueryRepository
    {
        private readonly SalesManagementDbContext _dbContext;

        public ProductQueryRepository(SalesManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductDetails> GetProductByIdAsync(Guid productId)
        {
            return await _dbContext.ProductDetails.FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<List<ProductDetails>> GetAllProductsAsync()
        {
            return await _dbContext.ProductDetails.ToListAsync();
        }

        public async Task<ProductCategory> GetCategoryByCategoryIdAsync(Guid categoryId)
        {
            return await _dbContext.ProductCategories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        public async Task<List<ProductCategory>> GetAllCategoriesAsync()
        {
            return await _dbContext.ProductCategories.ToListAsync();
        }
    }
}
