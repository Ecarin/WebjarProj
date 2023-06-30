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

                if (featureIds is not null && featureIds.Count > 0)
                {
                    foreach (var featureId in featureIds)
                    {
                        var productFeature = new ProductFeature()
                        {
                            ProductId = product.ProductId,
                            FeatureId = featureId,
                        };
                        await _dbContext.ProductFeatures.AddAsync(productFeature);
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

        public async Task<List<Product>> GetAllProductsAsync(
            string? name = null,
            string? priceType = null,
            List<int>? featureIds = null,
            bool withDiscounts = false,
            bool sortByPrice = true)
        {
            IQueryable<Product> query = _dbContext.Products
                .Include(p => p.ProductFeatures)
                .ThenInclude(pf => pf.Feature);

            // Apply filters
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(priceType))
            {
                query = query.Where(p => p.PriceType == priceType);
            }

            if (featureIds != null && featureIds.Count > 0)
            {
                query = query.Where(p => p.ProductFeatures.Any(pf => featureIds.Contains(pf.FeatureId)));
            }

            if (withDiscounts)
            {
                query = query.Where(p => p.DiscountAmount != null &&
                (p.DiscountExpireAt == null || p.DiscountExpireAt >= DateTime.UtcNow));
            }

            // Sort by price
            if (sortByPrice)
            {
                query = query.OrderBy(p => p.Price);
            }

            // Execute the query and return the results
            return await query.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
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
