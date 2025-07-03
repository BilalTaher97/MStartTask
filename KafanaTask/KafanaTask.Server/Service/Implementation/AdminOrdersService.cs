using KafanaTask.Server.Models;
using KafanaTask.Server.Repository.Interface;
using KafanaTask.Server.Service.Interface;

namespace KafanaTask.Server.Service.Implementation
{
    public class AdminOrdersService : IAdminOrdersService
    {
        private readonly IAdminOrdersRepos _adminOrdersRepos;
        public AdminOrdersService(IAdminOrdersRepos adminOrdersRepos)
        {
            _adminOrdersRepos = adminOrdersRepos;
        }


        public async Task<(List<Order> orders, int TotalCount)> GetOrders(int page, int pageSize)
        {
            var orders = await _adminOrdersRepos.GetAllOrdersAsync(page, pageSize);
            var totalCount = await _adminOrdersRepos.OrderCount();

            return (orders, totalCount);
        }


        public async Task<int> GetOrderCountByCustomerIdAsync(int customerId)
        {
            return await _adminOrdersRepos.GetOrderCountByCustomerId(customerId);
        }


    }
}
