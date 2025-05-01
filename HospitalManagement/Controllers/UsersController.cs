using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.RateLimiting;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableRateLimiting("fixed")]
    public class UsersController : ODataController
    {


        private static List<User> Users = new List<User>
        {
            new User { Id = 1, Name = "Andrew", Age = 25 },
            new User { Id = 2, Name = "Alice", Age = 30 },
            new User { Id = 3, Name = "Bob", Age = 20 }
        };

        [HttpGet]
        [EnableQuery] // OData qo‘llash
       // [EnableRateLimiting("slicing")]
        public IActionResult GetUsers()
        {
            return Ok(Users.AsQueryable());
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    
}
