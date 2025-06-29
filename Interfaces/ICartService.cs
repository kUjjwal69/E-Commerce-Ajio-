using E_Commerce_Project.Models;

namespace E_Commerce_Project.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartItem>> GetCartItems(int userId);
        Task AddToCart(int userId, int productId, int quantity);
        Task RemoveFromCart(int cartItemId);
        Task ClearCart(int userId);
    }

}
