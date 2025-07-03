using KafanaTask.Server.Models;

namespace KafanaTask.Server.Service.Interface
{
    public interface IAdminOrdersService
    {
        Task<(List<Order> orders, int TotalCount)> GetOrders(int page, int pageSize);
        Task<int> GetOrderCountByCustomerIdAsync(int customerId);
    }
}
