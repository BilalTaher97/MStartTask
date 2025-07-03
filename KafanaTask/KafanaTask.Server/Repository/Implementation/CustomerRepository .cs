using KafanaTask.DTOs;
using KafanaTask.Repository.Interface;
using KafanaTask.Server.DTOs;
using KafanaTask.Server.Models;
using Microsoft.EntityFrameworkCore;


namespace KafanaTask.Repository.Implemetnation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MyDbContext _context;

        public CustomerRepository(MyDbContext context)
        {
            _context = context;
        }



        public async Task<List<CustomerDTO_1>> GetAllUsersAsync(int page, int pageSize)
        {
            return await _context.Customers
                .Include(c => c.Orders) 
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CustomerDTO_1
                {
                    Id = c.Id,
                    NameEn = c.NameEn,
                    NameAr = c.NameAr,
                    Email = c.Email,
                    Phone = c.Phone,
                    StatusEn = c.StatusEn,
                    StatusAr = c.StatusAr,
                    GenderEn = c.GenderEn,
                    GenderAr = c.GenderAr,
                    DateOfBirth = c.DateOfBirth.HasValue? c.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue): null,
                    ServerDateTime = c.ServerDateTime,
                    DateTimeUtc = c.DateTimeUtc,
                    LastLoginDateTimeUtc = c.LastLoginDateTimeUtc,
                    UpdateDateTimeUtc = c.UpdateDateTimeUtc,
                    Photo = !string.IsNullOrEmpty(c.Photo) ? Convert.FromBase64String(c.Photo) : null,
                    OrdersCount = c.Orders.Count
                })
                .ToListAsync();
        }



        public async Task<int> usersCount()
        {
            return await _context.Customers.CountAsync();
        }

        public async Task<Customer?> GetUserById(int Id)
        {
            var currentUser = await _context.Customers.FindAsync(Id);

            return currentUser;
        }

        public async Task<List<Customer>> GetUsersByIdsAsync(List<int> ids)
        {
            return await _context.Customers
                .Include(c => c.Orders)
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();
        }

        public async Task DeleteUsersAsync(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                _context.Orders.RemoveRange(customer.Orders); 
            }

            _context.Customers.RemoveRange(customers); 
            await _context.SaveChangesAsync();
        }




        public async Task<Customer?> UpdateStatusAsync(int id, string statusEn, string statusAr)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return null;

            customer.StatusEn = statusEn;
            customer.StatusAr = statusAr;
            customer.UpdateDateTimeUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return customer;
        }


        public async Task<bool> Add(Customer customer)
        {
            _context.Customers.Add(customer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {

            return await _context.Customers.FirstOrDefaultAsync(x => x.Email == email);


        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<List<Order>> GetOrderById(int Cust_Id)
        {

            return await _context.Orders.Where(O => O.CustomerId == Cust_Id).ToListAsync();


        }

        public async Task<bool> CancelOrder(int Id)
        {

            var Order = await _context.Orders.FirstOrDefaultAsync(Ord => Ord.Id == Id);


            if (Order == null) return false;


            Order.StatusEn = "Inactive";

            Order.StatusAr = "غير نشط";

            _context.Orders.Update(Order);

            if (_context.SaveChanges() > 0)
            {
                return true;
            }

            return false;

        }


    }
}
