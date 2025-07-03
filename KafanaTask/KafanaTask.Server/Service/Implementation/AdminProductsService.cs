using KafanaTask.Server.DTOs;
using KafanaTask.Server.Models;
using KafanaTask.Server.Repository.Interface;
using KafanaTask.Server.Service.Interface;

namespace KafanaTask.Server.Service.Implementation
{
    public class AdminProductsService : IAdminProductsService
    {
        private readonly IAdminProductsRepo _adminProductsRepo;

        public AdminProductsService(IAdminProductsRepo adminProductsRepo)
        {
            _adminProductsRepo = adminProductsRepo;
        }


        public async Task<(List<Product> products, int TotalCount)> GetProductsAsync(int page, int pageSize)
        {
            var products = await _adminProductsRepo.GetAllProductsAsync(page, pageSize);
            var totalCount = await _adminProductsRepo.ProductsCount();

            return (products, totalCount);
        }


        public async Task<Product> AddProductAsync(ProductCreateDto dto)
        {
            var product = new Product
            {
                NameEn = dto.NameEn,
                NameAr = dto.NameAr,
                DescriptionEn = dto.DescriptionEn,
                DescriptionAr = dto.DescriptionAr,
                StatusEn = dto.StatusEn,
                StatusAr = dto.StatusAr,
                Amount = dto.Amount,
                Currency = dto.Currency,
                ServerDateTime = DateTime.Now,
                DateTimeUtc = DateTime.UtcNow
            };

            return await _adminProductsRepo.AddProductAsync(product);
        }


        public async Task<Product?> UpdateStatusAsync(int id, string statusEn, string statusAr)
        {
            return await _adminProductsRepo.UpdateStatusAsync(id, statusEn, statusAr);
        }



    }
}
