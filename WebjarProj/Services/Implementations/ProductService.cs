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
                        // var product_Feature = new Product_Feature()
                        // {
                        //     ProductId = productId,
                        //     FeatureId = item,
                        // };
                        // await _dbContext.Product_Features.AddAsync(product_Feature);
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
            IQueryable<Product> products = _dbContext.Products.Include(p => p.Features);

            // Apply filters based on the input parameters
            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(priceType))
            {
                products = products.Where(p => p.PriceType == priceType);
            }

            if (featureIds != null && featureIds.Any())
            {
                products = products
                    .Where(p => p.Features.Any(pf => featureIds.Contains(pf.FeatureId)));
            }

            if (withDiscounts)
            {
                products = products.Where(p => p.DiscountAmount != null && (p.DiscountExpireAt == null || p.DiscountExpireAt > DateTime.Now));
            }

            // Sort the products by price if specified
            if (sortByPrice)
            {
                products = products.OrderBy(p => p.Price);
            }

            // Retrieve the filtered and sorted products from the database
            var result = await products.ToListAsync();

            // Return the filtered and sorted products
            return result;
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
