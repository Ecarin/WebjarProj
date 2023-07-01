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

        public async Task DeleteProductByIdAsync(int productId)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                // Find the product in the database
                var product = await _dbContext.Products
                    .Include(p => p.ProductFeatures)
                    .FirstOrDefaultAsync(p => p.ProductId == productId);

                if (product is null)
                {
                    // Handle the case when the product does not exist
                    throw new Exception("Product not found.");
                }

                // Remove associated product features
                _dbContext.ProductFeatures.RemoveRange(product.ProductFeatures);

                // Remove the product
                _dbContext.Products.Remove(product);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
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
            return await _dbContext.Products
                .Include(p => p.ProductFeatures)
                .ThenInclude(pf => pf.Feature)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task UpdateProductAsync(
            Product updatedProduct,
            List<int>? featureIds = null)
        {
            // transactions for database safety
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                // Find the existing product in the database
                var existingProduct = await _dbContext.Products
                    .Include(p => p.ProductFeatures)
                    .FirstOrDefaultAsync(p => p.ProductId == updatedProduct.ProductId);

                if (existingProduct == null)
                {
                    // Handle the case when the product does not exist
                    throw new Exception("Product not found.");
                }

                // Update the properties of the existing product
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Image = updatedProduct.Image;
                existingProduct.PriceType = updatedProduct.PriceType;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.DiscountAmount = updatedProduct.DiscountAmount;
                existingProduct.DiscountExpireAt = updatedProduct.DiscountExpireAt;
                existingProduct.Quantity = updatedProduct.Quantity;

                // Remove existing product features
                existingProduct.ProductFeatures.Clear();

                if (featureIds is not null && featureIds.Any())
                {
                    // Add the updated product features
                    foreach (var featureId in featureIds)
                    {
                        var newFeature = new ProductFeature()
                        {
                            ProductId = updatedProduct.ProductId,
                            FeatureId = featureId,
                        };
                        existingProduct.ProductFeatures.Add(newFeature);
                    }
                }
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
