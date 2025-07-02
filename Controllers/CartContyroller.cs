using E_Commerce_Project.Interfaces;
using E_Commerce_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartContyroller : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartContyroller(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartItem cartItem)
        {
            await _cartService.AddToCart(cartItem.UserId,cartItem.ProductId,cartItem.Quantity);
            return Ok("Items added to cart Successfully");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartItems(int userId)
        {
           var cartItems= await _cartService.GetCartItems(userId);
            return Ok(cartItems);

        }
        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            await _cartService.RemoveFromCart(cartItemId);
            return Ok("Items removed from the cart..");
        }
        [HttpDelete("delete/{userId")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            await _cartService.ClearCart(userId);
            return Ok("All carts are cleared now");
        }


    }
}
