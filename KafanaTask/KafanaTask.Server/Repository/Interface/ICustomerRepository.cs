
using KafanaTask.DTOs;
using KafanaTask.Server.DTOs;
using KafanaTask.Server.Models;

namespace KafanaTask.Repository.Interface
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDTO_1>> GetAllUsersAsync(int page, int pageSize);
        Task<int> usersCount();
        Task<Customer> GetUserById(int Id);
        Task<List<Customer>> GetUsersByIdsAsync(List<int> ids);
        Task DeleteUsersAsync(List<Customer> customers);
        Task<Customer?> UpdateStatusAsync(int id, string statusEn, string statusAr);



        Task<bool> Add(Customer customer);

        Task<Customer> GetCustomerByEmail(string email);

        Task<List<Product>> GetAllProducts();

        Task<List<Order>> GetOrderById(int Id);


        Task<bool> CancelOrder (int Id);

    }
}
