using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //GET:http//localhost:portnumber/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] name = new string[] { "gaurav", "shraddhu", "hii" };
            return Ok(name);
            //Ok will return http 200 code response and we are returning this to the swagger  
        }
    }
}
