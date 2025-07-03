using KafanaTask.Server.DTOs;
using KafanaTask.Server.Models;
using KafanaTask.Server.Repository.Interface;
using KafanaTask.Server.Service.Interface;

namespace KafanaTask.Server.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;

        public OrderService(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task<Order> CreateOrderAsync(OrderCreateDto dto)
        {
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                ProductId = dto.ProductId,
                TotalAmount = dto.TotalAmount,
                Currency = dto.Currency,
                StatusEn = "Approved",
                StatusAr = "تمت الموافقه",
                ServerDateTime = DateTime.Now,
                DateTimeUtc = DateTime.UtcNow
            };

            return await _orderRepo.CreateOrderAsync(order);
        }
    }
}
