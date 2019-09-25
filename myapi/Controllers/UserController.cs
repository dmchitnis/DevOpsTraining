using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace myapi.Controllers
{
    public class userLogin {
        public string userName;
        public string password;
        public string token;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        // POST api/user
        [HttpPost("login")]
        public ActionResult<userLogin> Login([FromBody] userLogin value)
        {
            value.token = value.password + value.userName;
            return Ok(value);
        }
    }
}
