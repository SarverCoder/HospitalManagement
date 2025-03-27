using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        public DoctorsController()
        {
            
        }

        //[HttpGet]
        //public Task<IActionResult> GetDoctors()
        //{
        //    return Ok("List of doctors");
        //}
    }
}
