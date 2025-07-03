using KafanaTask.DTOs;
using KafanaTask.Server.DTOs;
using KafanaTask.Server.Models;

namespace KafanaTask.Service.Interface
{
    public interface ICustomerService
    {


        Task<bool> AddUser(Customer customer);

        Task<string> LoginAsync(string email, string password);

        Task<List<Product>> GetProducts();

        Task<List<Order>> GetOrderById(int Id);


        Task<bool> CancelOrder(int Id);


        Task<(List<CustomerDTO_1> customers, int TotalCount)> GetPaginatedAsync(int page, int pageSize);

        Task<Customer?> GetUserByIdAsync(int id);
        Task DeleteUsersAsync(List<int> ids);
        Task<Customer?> UpdateStatusAsync(int id, string statusEn, string statusAr);

    }
}
