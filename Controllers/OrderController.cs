using E_Commerce_Project.Interfaces;
using E_Commerce_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/order/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderById(id);
            return Ok(order);
        }

        // GET: api/order/user/3
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(int userId)
        {
            var orders = await _orderService.GetOrdersByUser(userId);
            return Ok(orders);
        }

        // POST: api/order/place-order
        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder(int userId, Address address, string paymentMethod)
        {
            var placedOrder = await _orderService.PlaceOrder(userId, address, paymentMethod);
            return Ok(placedOrder);
        }

        // PUT: api/order/update-status
        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, string status)
        {
            await _orderService.UpdateOrderStatus(orderId, status);
            return Ok("Order status updated successfully.");
        }
    }
}
