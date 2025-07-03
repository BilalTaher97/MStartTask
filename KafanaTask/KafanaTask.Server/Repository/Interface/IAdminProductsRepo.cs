using KafanaTask.Server.Models;

namespace KafanaTask.Server.Repository.Interface
{
    public interface IAdminProductsRepo
    {
        Task<List<Product>> GetAllProductsAsync(int page, int sizePage);

        Task<int> ProductsCount();

        Task<Product> AddProductAsync(Product product);
        Task<Product?> UpdateStatusAsync(int id, string statusEn, string statusAr);
    }
}
