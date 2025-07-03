using KafanaTask.Server.Models;

namespace KafanaTask.Server.Repository.Interface
{
    public interface IAdminOrdersRepos
    {
        Task<List<Order>> GetAllOrdersAsync(int page, int sizePage);

        Task<int> OrderCount();

        Task<int> GetOrderCountByCustomerId(int customerId);
    }
}
