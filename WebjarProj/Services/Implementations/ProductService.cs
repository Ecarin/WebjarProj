using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebjarProj.Data;
using WebjarProj.Models;
using WebjarProj.Models.Requests;
using WebjarProj.Services.Interfaces;

namespace WebjarProj.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly WebjarDbContext _dbContext;

        public ProductService(WebjarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateProductAsync(Product product, List<int>? featureIds = null)
        {
            // transactions for database safety
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();

                // Retrieve the created product_id
                int productId = product.Id;
                if (featureIds is not null && featureIds.Count > 0)
                {
                    foreach (var item in featureIds)
                    {
                        var product_Feature = new Product_Feature()
                        {
                            ProductId = productId,
                            FeatureId = item,
                        };
                        await _dbContext.Product_Features.AddAsync(product_Feature);
                    }
                }
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteProductAsync(int id)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var product = await _dbContext.Products.FindAsync(id);
                if (product is null)
                    throw new ArgumentException("Product not found.");

                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(int? priceType = null, List<int>? featureIds = null, List<int>? addonIds = null, bool hasActiveDiscount = false, bool sortByPrice = true)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                return await _dbContext.Products.ToListAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Product> GetProductByIdAsync(int id, List<int>? addonIds = null)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                return await _dbContext.Products.FindAsync(id);

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
