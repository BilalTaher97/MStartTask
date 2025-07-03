using KafanaTask.DTOs;
using KafanaTask.Server.DTOs;
using KafanaTask.Server.Models;
using KafanaTask.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KafanaTask.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> AddNewUser([FromForm] RegisterCustomerDto dto)
        {
            bool IsArabic(string input)
            {
                if (string.IsNullOrEmpty(input))
                    return false;

                return input.Any(c => (c >= 0x0600 && c <= 0x06FF) ||
                                      (c >= 0x0750 && c <= 0x077F) ||
                                      (c >= 0x08A0 && c <= 0x08FF));
            }

            (string GenderAr, string GenderEn) ConvertGender(string input)
            {
                if (string.IsNullOrWhiteSpace(input))
                    return ("غير معروف", "Unknown");

                if (IsArabic(input))
                {
                    switch (input.Trim())
                    {
                        case "ذكر": return ("ذكر", "Male");
                        case "أنثى": return ("أنثى", "Female");
                        default: return (input, "Unknown");
                    }
                }
                else
                {
                    switch (input.Trim().ToLower())
                    {
                        case "male": return ("ذكر", "Male");
                        case "female": return ("أنثى", "Female");
                        default: return ("غير معروف", input);
                    }
                }
            }

            var (genderAr, genderEn) = ConvertGender(dto.GenderInput);

            var nowUtc = DateTime.UtcNow;

            string photoFileName = null;

          
            if (dto.Photo != null && dto.Photo.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                photoFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Photo.FileName);
                var filePath = Path.Combine(uploadsFolder, photoFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Photo.CopyToAsync(stream);
                }
            }

            var customer = new Customer
            {
                NameEn = dto.NameEn ?? "",
                NameAr = dto.NameAr ?? "",
                Email = dto.Email ?? "",
                Phone = dto.Phone ?? "",
                Photo = photoFileName ?? "",
                PasswordHash = ComputeSha256Hash(dto.Password ?? ""),

                GenderAr = genderAr,
                GenderEn = genderEn,

                StatusAr = "نشط",
                StatusEn = "Active",

                LastLoginDateTimeUtc = nowUtc,
                UpdateDateTimeUtc = nowUtc,
                DateOfBirth = dto.DateOfBirth.HasValue ? DateOnly.FromDateTime(dto.DateOfBirth.Value) : null
            };

            bool result = await _customerService.AddUser(customer);

            if (result)
            {
                return Ok(new { message = "User Added Successfully" });
            }
            else
            {
                return BadRequest(new { message = "Failed to Add User" });
            }

        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2"));

                return builder.ToString();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDTO LoginData)
        {

            var token = await _customerService.LoginAsync(LoginData.Email, LoginData.Password);

            if (token == null)
                return Unauthorized("Invalid credentials");

            return Ok(new { Token = token });

        }

        [Authorize]
        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _customerService.GetProducts();

            if (products == null || !products.Any())
            {
                return NotFound();
            }

            var result = products.Select(p => new
            {
                name = new { en = p.NameEn, ar = p.NameAr },
                description = new { en = p.DescriptionEn, ar = p.DescriptionAr },
                isActive = p.StatusEn,
                amount = p.Amount,
                currency = p.Currency
            });

            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetOrdersByCustomerID")]
        public async Task<IActionResult> GetOrdersByCustomerID(int customerId)
        {
            var orders = await _customerService.GetOrderById(customerId);

            if (orders == null || !orders.Any())
            {
                return NotFound();
            }

            return Ok(orders);
        }

        [Authorize]
        [HttpPatch("{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            bool isCancelled = await _customerService.CancelOrder(orderId);

            if (!isCancelled)
            {
                return BadRequest(new { Message = "Unable to cancel the order. It may not exist or cannot be cancelled." });
            }

            return Ok(new { OrderId = orderId, Status = "Cancelled" });
        }


    }
}
