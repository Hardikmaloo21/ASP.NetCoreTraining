using FoodOrdering.Web.Data;
using FoodOrdering.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using System.IO;

namespace FoodOrdering.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var foods = await _context.FoodItems.ToListAsync();

            return View(foods);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
public async Task<IActionResult> Create(
    FoodItem food,
    IFormFile imageFile)
{
    if (imageFile != null)
    {
        var fileName = Guid.NewGuid()
            + Path.GetExtension(imageFile.FileName);

        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot/images",
            fileName);

        using var stream =
            new FileStream(path, FileMode.Create);

        await imageFile.CopyToAsync(stream);

        food.ImageUrl = "/images/" + fileName;
    }

    _context.FoodItems.Add(food);

    await _context.SaveChangesAsync();

    return RedirectToAction("Index");
}

        public async Task<IActionResult> Delete(int id)
        {
            var food = await _context.FoodItems.FindAsync(id);

            _context.FoodItems.Remove(food);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
{
    var food = await _context.FoodItems.FindAsync(id);

    return View(food);
}

[HttpPost]
public async Task<IActionResult> Edit(FoodItem food)
{
    _context.FoodItems.Update(food);

    await _context.SaveChangesAsync();

    return RedirectToAction("Index");
}
    }
}