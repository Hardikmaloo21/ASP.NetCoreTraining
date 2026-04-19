using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAttendanceManagementSystem.model;

namespace StudentAttendanceManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private static List<Attendance> attendanceRecords = new();
        [HttpGet]
        public IActionResult GetAttendance() => Ok(attendanceRecords);

        [HttpPost]
        public IActionResult Mark(Attendance attendance)
        {
            attendanceRecords.Add(attendance);
            return Ok(attendanceRecords);
        }
    }
}
