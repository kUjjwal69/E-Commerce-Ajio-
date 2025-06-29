using E_Commerce_Project.Models;

namespace E_Commerce_Project.Interfaces
{
    public interface IOrderService
    {
        Task<Order> PlaceOrder(int userId, Address address, string paymentMethod);
        Task<IEnumerable<Order>> GetOrdersByUser(int userId);
        Task<Order> GetOrderById(int id);
        Task UpdateOrderStatus(int orderId, string status);
    }

}
