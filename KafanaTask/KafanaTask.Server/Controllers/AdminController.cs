using KafanaTask.Server.DTOs;
using KafanaTask.Server.Models;
using KafanaTask.Server.Service.Implementation;
using KafanaTask.Server.Service.Interface;
using KafanaTask.Service.Implemetnation;
using KafanaTask.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KafanaTask.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAdminProductsService _proService;
        private readonly IAdminOrdersService _orderService;


        public AdminController(ICustomerService service, IAdminProductsService proService, IAdminOrdersService orderService)
        {
            _customerService = service;
            _proService = proService;
            _orderService = orderService;
        }


        [HttpGet("customers")]
        public async Task<IActionResult> GetPaginatedCustomers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var(customer, totalCount) = await _customerService.GetPaginatedAsync(page, pageSize);


            return Ok( new
                {
                    data = customer,
                    totalUsers = totalCount,
                    currentPage = page,
                    thePageSize = pageSize

                }

                );

        }


        [HttpGet("customers/{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetUserByIdAsync(id);

            return Ok(customer);
        }



        [HttpDelete("customers")]
        public async Task<IActionResult> DeleteCustomers([FromBody] List<int> ids)
        {
            await _customerService.DeleteUsersAsync(ids);
            return Ok(new { message = "Customers deleted successfully." });
        }




        [HttpPut("update-status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] CustomerStatusUpdateDto dto)
        {
            var updatedCustomer = await _customerService.UpdateStatusAsync(id, dto.StatusEn, dto.StatusAr);

            if (updatedCustomer == null)
                return NotFound();

            return Ok(updatedCustomer);
        }











        [HttpGet("products")]
        public async Task<IActionResult> GetPaginatedProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (products, totalCount) = await _proService.GetProductsAsync(page, pageSize);

            var result = new
            {
                data = products,
                total = totalCount,
                currentPage = page,
                thePageSize = pageSize
            };

            return Ok(result);
        }


        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto dto)
        {
            var addedProduct = await _proService.AddProductAsync(dto);
            return Ok(addedProduct); // أو CreatedAtAction(...) لو بدك تتبع REST بشكل أدق
        }



        [HttpPut("update-product-status/{id}")]
        public async Task<IActionResult> UpdateProductStatus(int id, [FromBody] ProductStatusUpdateDto dto)
        {
            var updated = await _proService.UpdateStatusAsync(id, dto.StatusEn, dto.StatusAr);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }







        [HttpGet("orders")]
        public async Task<IActionResult> GetPaginatedOrders([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (order, totalCount) = await _orderService.GetOrders(page, pageSize);

            return Ok(new
            {
                data = order,
                total = totalCount,
                currentPage = page,
                thePageSize = pageSize
            });
        }


        [HttpGet("customers/{customerId}/order-count")]
        public async Task<IActionResult> GetCustomerOrderCount(int customerId)
        {
            var count = await _orderService.GetOrderCountByCustomerIdAsync(customerId);
            return Ok(count);
        }



    }
}
