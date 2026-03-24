using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseManagementAPI.Data;
using CourseManagementAPI.Models;

namespace CourseManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrollmentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> Enroll(Enrollment enrollment)
        {
            if (enrollment.CourseId <= 0 || string.IsNullOrEmpty(enrollment.StudentName))
                return BadRequest("Invalid enrollment data");

            enrollment.EnrollmentId = 0;
            enrollment.EnrollmentDate = DateTime.Now;

            _context.Enrollments.Add(enrollment);

            await _context.SaveChangesAsync();

            return Ok(enrollment);
        }
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteEnrollment(int id)
{
    var enrollment = await _context.Enrollments.FindAsync(id);

    if (enrollment == null)
        return NotFound();

    _context.Enrollments.Remove(enrollment);

    await _context.SaveChangesAsync();

    return Ok("Enrollment deleted");
}
        [HttpGet]
        public async Task<IEnumerable<Enrollment>> GetEnrollments()
        {
            return await _context.Enrollments.ToListAsync();
        }
    }
}