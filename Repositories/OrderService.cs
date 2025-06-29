using E_Commerce_Project.Context;
using E_Commerce_Project.Interfaces;
using E_Commerce_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Project.Repositories
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly ICartService _cartService;

        public OrderService(AppDbContext context, ICartService CartService)
        {
            _context = context;
            _cartService = CartService;
        }

        public async Task<Order> GetOrderById(int id)
        {
           var order =  await _context.Orders.FindAsync(id);
            return order;   
        }

        public async Task<IEnumerable<Order>> GetOrdersByUser(int userId)
        {
          var order =  await _context.Orders.Where(o => o.UserId == userId)
                .Include(i=>i.OrderItems)// we added include to add aditional items related to orders, although this is optional
                .ToListAsync();
            await _context.SaveChangesAsync();
            return order;
        }

       

        public async Task<Order> PlaceOrder(int userId, Address address, string paymentMethod)
        {
            // 1. Get user's cart items from CartRepository
            var cartItems = await _cartService.GetCartItems(userId);

            // 2. Check if cart is empty
            if (cartItems == null || !cartItems.Any())
            {
                return null; // Nothing to order
            }

            // 3. Calculate total amount
            decimal totalAmount = cartItems.Sum(ci => ci.Product.Price * ci.Quantity);

            // 4. Create PaymentDetail
            // payment details are added just to ensure that in future we can locate about all the details like timestamps, payment failed or succeded
            var paymentDetail = new PaymentDetail
            {
                Method = paymentMethod,
                Status = "Pending",         // or "Success" if instant
                TransactionId = Guid.NewGuid().ToString() // it gives you a unique string of id each time
            };

            // 5. Create order and fill details
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = "Placed",
                ShippingAddress = address,
                PaymentDetail = paymentDetail,
                TotalAmount = totalAmount,
                OrderItems = new List<OrderItem>()
            };

            // 6. Convert cart items to order items
            foreach (var item in cartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                });
            }

            // 7. Save the order
            _context.Orders.Add(order);

            // 8. Now we will  Clear user's cart
            _context.CartItems.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            return order;
        }


        public async Task UpdateOrderStatus(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();
            }
        }


    }
}
