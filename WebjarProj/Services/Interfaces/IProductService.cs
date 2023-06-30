
using System.Collections.Generic;
using System.Threading.Tasks;
using WebjarProj.Models;
using WebjarProj.Models.Requests;

namespace WebjarProj.Services.Interfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(Product product, List<int>? featureIds = null);

        Task<Product> GetProductByIdAsync(int id, List<int>? addonIds = null);

        Task<IEnumerable<Product>> GetAllProductsAsync(
            int? priceType = null,
            List<int>? featureIds = null,
             List<int>? addonIds = null,
             bool hasActiveDiscount = false,
             bool sortByPrice = true);

        Task UpdateProductAsync(Product product);

        Task DeleteProductAsync(int id);
    }

}
