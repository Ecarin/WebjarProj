
using System.Collections.Generic;
using System.Threading.Tasks;
using WebjarProj.Models;
using WebjarProj.Models.Requests;

namespace WebjarProj.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(Product product, List<int>? featureIds = null);

        Task<Product> GetProductByIdAsync(int id);

        Task<List<Product>> GetAllProductsAsync(
            string? name = null,
            string? priceType = null,
            List<int>? featureIds = null,
            bool withDiscounts = false,
            bool sortByPrice = true);

        Task UpdateProductAsync(Product product);

        Task DeleteProductAsync(int id);
    }

}
