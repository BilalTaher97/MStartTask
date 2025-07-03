using KafanaTask.Server.Models;

namespace KafanaTask.Server.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(Order order);
    }
}
