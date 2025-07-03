using KafanaTask.Server.Models;
using KafanaTask.Server.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace KafanaTask.Server.Repository.Implementation
{
    public class AdminProductsRepo : IAdminProductsRepo
    {

        private readonly MyDbContext _context;

        public AdminProductsRepo(MyDbContext context)
        {
            _context = context;
        }


        public async Task<List<Product>> GetAllProductsAsync(int page, int sizePage)
        {
            return await _context.Products
                .Skip((page - 1) * sizePage)
                .Take(sizePage)
                .ToListAsync();
        }


        public async Task<int> ProductsCount()
        {
            return await _context.Products.CountAsync();
        }


        public async Task<Product> AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product; 
        }


        public async Task<Product?> UpdateStatusAsync(int id, string statusEn, string statusAr)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            product.StatusEn = statusEn;
            product.StatusAr = statusAr;
            product.UpdateDateTimeUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return product;
        }

    }
}
