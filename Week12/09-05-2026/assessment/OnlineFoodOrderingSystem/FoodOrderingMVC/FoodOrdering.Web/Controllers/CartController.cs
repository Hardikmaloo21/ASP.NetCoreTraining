using FoodOrdering.Web.Data;
using FoodOrdering.Web.Models;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // SHOW CART
        // =========================

        public async Task<IActionResult> Index()
        {
            var cart = await _context.CartItems
                .Include(c => c.FoodItem)
                .ToListAsync();

            return View(cart);
        }

        // =========================
        // ADD TO CART
        // =========================

        public async Task<IActionResult> AddToCart(int id)
        {
            var food = await _context.FoodItems.FindAsync(id);

            if (food == null)
            {
                return NotFound();
            }

            var cartItem = new CartItem
            {
                FoodItemId = food.Id,
                Quantity = 1
            };

            _context.CartItems.Add(cartItem);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // =========================
        // CHECKOUT
        // =========================

        public async Task<IActionResult> Checkout()
        {
            var cartItems = await _context.CartItems
                .Include(c => c.FoodItem)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return RedirectToAction("Index");
            }

            decimal total = cartItems.Sum(c =>
                c.Quantity * c.FoodItem.Price);

            var order = new Order
            {
                UserEmail = "customer@gmail.com",
                TotalAmount = total,
                Status = "Placed"
            };

            _context.Orders.Add(order);

            _context.CartItems.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            return RedirectToAction(
                "Index",
                "Order");
        }
    }
}