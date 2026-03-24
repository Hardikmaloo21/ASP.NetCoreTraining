using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SalesManagementAPI.Data;
using SalesManagementAPI.Models;
using SalesManagementAPI.DTOs;

namespace SalesManagementAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        private static readonly string[] ValidRoles = { "Admin", "Manager", "SalesRep" };

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        //Get all users
        // GET api/admin/users
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    Role = u.Role,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt
                })
                .ToList();

            return Ok(users);
        }

        // Assign role to a user 
        // PUT api/admin/assign-role
        [HttpPut("assign-role")]
        public IActionResult AssignRole(AssignRoleDto dto)
        {
            var adminIdClaim = User.FindFirst("UserId")?.Value;

            if (adminIdClaim == null)
                return Unauthorized();

            int adminId = int.Parse(adminIdClaim);

            // Admin cannot change their own role
            if (dto.UserId == adminId)
                return BadRequest(new { message = "You cannot change your own role." });

            if (!ValidRoles.Contains(dto.Role))
                return BadRequest(new { message = $"Invalid role. Valid roles: {string.Join(", ", ValidRoles)}" });

            var user = _context.Users.Find(dto.UserId);

            if (user == null)
                return NotFound(new { message = "User not found." });

            var previousRole = user.Role;
            user.Role = dto.Role;

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = adminId,
                Action = "ROLE_ASSIGNED",
                Details = $"Admin {adminId} changed {user.Email} role from '{previousRole}' to '{dto.Role}'.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(new { message = $"Role updated to '{dto.Role}' for {user.Email}." });
        }

        // Deactivate user 
        // PUT api/admin/deactivate/{id}
        [HttpPut("deactivate/{id}")]
        public IActionResult DeactivateUser(int id)
        {
            var adminIdClaim = User.FindFirst("UserId")?.Value;

            if (adminIdClaim == null)
                return Unauthorized();

            int adminId = int.Parse(adminIdClaim);

            if (id == adminId)
                return BadRequest(new { message = "You cannot deactivate your own account." });

            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound(new { message = "User not found." });

            user.IsActive = false;

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = adminId,
                Action = "USER_DEACTIVATED",
                Details = $"Admin {adminId} deactivated user {user.Email}.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(new { message = $"User {user.Email} has been deactivated." });
        }

        // Reactivate user
        // PUT api/admin/activate/{id}
        [HttpPut("activate/{id}")]
        public IActionResult ActivateUser(int id)
        {
            var adminIdClaim = User.FindFirst("UserId")?.Value;

            if (adminIdClaim == null)
                return Unauthorized();

            int adminId = int.Parse(adminIdClaim);

            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound(new { message = "User not found." });

            user.IsActive = true;

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = adminId,
                Action = "USER_ACTIVATED",
                Details = $"Admin {adminId} reactivated user {user.Email}.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(new { message = $"User {user.Email} has been reactivated." });
        }

        // Delete user 
        // DELETE api/admin/delete/{id}
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var adminIdClaim = User.FindFirst("UserId")?.Value;

            if (adminIdClaim == null)
                return Unauthorized();

            int adminId = int.Parse(adminIdClaim);

            if (id == adminId)
                return BadRequest(new { message = "You cannot delete your own account." });

            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound(new { message = "User not found." });

            _context.Users.Remove(user);

            _context.AuditLogs.Add(new AuditLog
            {
                ActingUserId = adminId,
                Action = "USER_DELETED",
                Details = $"Admin {adminId} deleted user {user.Email}.",
                CreatedAt = DateTime.UtcNow
            });

            _context.SaveChanges();

            return Ok(new { message = "User deleted successfully." });
        }

        // ── View audit logs ───────────────────────────────────────────────────
        // GET api/admin/audit-logs
        [HttpGet("audit-logs")]
        public IActionResult GetAuditLogs()
        {
            var logs = _context.AuditLogs
                .OrderByDescending(l => l.CreatedAt)
                .ToList();

            return Ok(logs);
        }
    }
}
