using E_Commerce_Project.Context;
using E_Commerce_Project.Interfaces;
using E_Commerce_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Project.Repositories
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }
        /*
         One user can have multiple cart items
          Each cart item is for one product
           If the same product is added again, update quantity
*/

        public async Task AddToCart(int userId, int productId, int quantity)
        {
            // at first we will check whether the existing item is present in the cart or not with the given userId and productId
            var existingCartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
            // by this we add that item by quantity
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
            }

            else
            {
                // then we have to create a fresh cart for that user
                var cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                };

                // incase if we have a new item that we want to add into the cart
                await _context.CartItems.AddAsync(cartItem);
            }
            await _context.SaveChangesAsync();
        }

        public async Task ClearCart(int userId)
        {
            // we can first check whether our cart is already empty or not,
            // if it is then we will not do anything else we will remove all items from cart

            // this lines of LINQ  checks whether of this userId has list of
            // items in the cart or not if from the carItems this userId does not matches
            // then we will do nothing else we will do the rest of the operation
            var isEmpty = await _context.CartItems.AnyAsync(c => c.UserId == userId);
            if (isEmpty)
            {
                var cartItems = await _context.CartItems.Where(x => x.UserId == userId).ToListAsync();
                _context.RemoveRange(cartItems);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<CartItem>> GetCartItems(int userId)
        {
            var allCartItems = await _context.CartItems.Where(a => a.UserId == userId).ToListAsync();
            return allCartItems;

        }

        public async Task RemoveFromCart(int cartItemId)
        {
            // at first we will check whether cartItem is present in the cart or not

            var isPresent = await _context.CartItems.FindAsync(cartItemId);

            if (isPresent != null)
            {
                _context.CartItems.Remove(isPresent);
                _context.SaveChanges();
            }
            // incase it's not present then no need to do anything as what yiu will remove if there is nothing?
        }
    }
}
