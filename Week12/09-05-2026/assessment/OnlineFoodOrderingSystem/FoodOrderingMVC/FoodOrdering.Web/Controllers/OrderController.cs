using Microsoft.AspNetCore.Mvc;
using FoodOrdering.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.ToListAsync();

            return View(orders);
        }
    }
}