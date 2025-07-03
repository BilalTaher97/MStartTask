using KafanaTask.Server.DTOs;
using KafanaTask.Server.Models;

namespace KafanaTask.Server.Service.Interface
{
    public interface IAdminProductsService
    {
        Task<(List<Product> products, int TotalCount)> GetProductsAsync(int page, int pageSize);
        Task<Product> AddProductAsync(ProductCreateDto dto);

        Task<Product?> UpdateStatusAsync(int id, string statusEn, string statusAr);
    }
}
