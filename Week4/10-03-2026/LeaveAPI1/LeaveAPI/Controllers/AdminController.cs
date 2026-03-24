using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LeaveAPI.Data;
using System.Linq;

namespace LeaveAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("employees")]
        public IActionResult GetEmployees()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound("User not found");

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok("User deleted successfully");
        }
    }
}