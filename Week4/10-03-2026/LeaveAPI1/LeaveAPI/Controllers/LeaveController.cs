using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LeaveAPI.Data;
using LeaveAPI.Models;
using LeaveAPI.DTOs;
using System.Security.Claims;

namespace LeaveAPI.Controllers
{
    [ApiController]
    [Route("api/leave")]
    public class LeaveController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeaveController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("apply")]
        public IActionResult ApplyLeave(ApplyLeaveDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            var leave = new LeaveRequest
            {
                EmployeeId = int.Parse(userIdClaim),
                LeaveType = dto.LeaveType,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Reason = dto.Reason,
                Status = "Pending"
            };

            _context.LeaveRequests.Add(leave);
            _context.SaveChanges();

            return Ok(leave);
        }

        [Authorize(Roles = "Employee")]
        [HttpGet("my-leaves")]
        public IActionResult MyLeaves()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim);

            var leaves = _context.LeaveRequests
                .Where(l => l.EmployeeId == userId)
                .ToList();

            return Ok(leaves);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("all")]
        public IActionResult AllLeaves()
        {
            return Ok(_context.LeaveRequests.ToList());
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("approve/{id}")]
        public IActionResult Approve(int id)
        {
            var leave = _context.LeaveRequests.Find(id);

            if (leave == null)
                return NotFound();

            leave.Status = "Approved";

            _context.SaveChanges();

            return Ok(leave);
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("reject/{id}")]
        public IActionResult Reject(int id)
        {
            var leave = _context.LeaveRequests.Find(id);

            if (leave == null)
                return NotFound();

            leave.Status = "Rejected";

            _context.SaveChanges();

            return Ok(leave);
        }
    }
}