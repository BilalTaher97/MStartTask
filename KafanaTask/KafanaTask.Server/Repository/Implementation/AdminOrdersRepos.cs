using KafanaTask.Server.Models;
using KafanaTask.Server.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace KafanaTask.Server.Repository.Implementation
{
    public class AdminOrdersRepos : IAdminOrdersRepos
    {
        private readonly MyDbContext _context;
        public AdminOrdersRepos(MyDbContext context)
        {
            _context = context;
      
        }

        public async Task<List<Order>> GetAllOrdersAsync(int page, int sizePage)
        {
            return await _context.Orders
                .Skip((page - 1) * sizePage)
                .Take(sizePage)
                .ToListAsync();
        }

        public async Task<int> OrderCount()
        {
            return await _context.Orders.CountAsync();
        }


        public async Task<int> GetOrderCountByCustomerId(int customerId)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .CountAsync();
        }




    }
}
