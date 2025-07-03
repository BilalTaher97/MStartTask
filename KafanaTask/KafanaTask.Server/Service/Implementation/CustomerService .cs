using KafanaTask.DTOs;
using KafanaTask.Repository.Interface;
using KafanaTask.Server.DTOs;
using KafanaTask.Server.JwtSetting;
using KafanaTask.Server.Models;
using KafanaTask.Service.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;


namespace KafanaTask.Service.Implemetnation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly string _secretKey;
        public CustomerService(ICustomerRepository customerRepository, IOptions<JwtSettings> jwtSettings)
        {
            _customerRepository = customerRepository;
            _secretKey = jwtSettings.Value.SecretKey;
        }

        public async Task<(List<CustomerDTO_1> customers, int TotalCount)> GetPaginatedAsync(int page, int pageSize)
        {

            var customers = await _customerRepository.GetAllUsersAsync(page, pageSize);
            var totalCount = await _customerRepository.usersCount();

            return (customers, totalCount);
        }

        public async Task<Customer?> GetUserByIdAsync(int id)
        {
            var customer = await _customerRepository.GetUserById(id);

            if (customer == null || customer.StatusEn != "Active")
                return null;


            return customer;
        }


        public async Task DeleteUsersAsync(List<int> ids)
        {
            var customers = await _customerRepository.GetUsersByIdsAsync(ids);
            if (!customers.Any())
                return;

            await _customerRepository.DeleteUsersAsync(customers);
        }



        public async Task<Customer?> UpdateStatusAsync(int id, string statusEn, string statusAr)
        {
            return await _customerRepository.UpdateStatusAsync(id, statusEn, statusAr);
        }


        public async Task<bool> AddUser(Customer customer)
        {
            return await _customerRepository.Add(customer);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var customer = await _customerRepository.GetCustomerByEmail(email);

            if (customer == null)
                return null;

            if (!VerifyPassword(password, customer.PasswordHash))
                return null;

            return GenerateJwtToken(customer);
        }



        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));

                return builder.ToString();
            }
        }


        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string enteredHash = ComputeSha256Hash(enteredPassword);
            return enteredHash == storedHash;
        }


        private string GenerateJwtToken(Customer customer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
            new Claim(ClaimTypes.Email, customer.Email),
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "yourAppName",
                Audience = "yourAppUsers",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task<List<Product>> GetProducts()
        {
            return await _customerRepository.GetAllProducts();
        }


        public async Task<List<Order>> GetOrderById(int Id)
        {
            return await _customerRepository.GetOrderById(Id);
        }

        public async Task<bool> CancelOrder(int Id)
        {
            return await _customerRepository.CancelOrder(Id);
        }


    }
}
