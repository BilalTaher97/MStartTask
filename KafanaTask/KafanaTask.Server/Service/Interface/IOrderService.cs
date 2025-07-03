using KafanaTask.Server.DTOs;
using KafanaTask.Server.Models;

namespace KafanaTask.Server.Service.Interface
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(OrderCreateDto dto);
    }
}
