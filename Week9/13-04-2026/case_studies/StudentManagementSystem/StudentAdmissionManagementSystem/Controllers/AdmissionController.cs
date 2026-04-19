using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdmissionManagementSystem.model;

namespace StudentAdmissionManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        
            private static List<Admission> admissions = new();

            [HttpGet]
            public IActionResult Get() => Ok(admissions);

            [HttpPost]
            public IActionResult Add(Admission admission)
            {
                admissions.Add(admission);
                return Ok(admissions);
            }



        }
    
}
