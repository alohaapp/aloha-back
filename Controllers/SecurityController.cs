using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Aloha.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aloha.Controllers
{
    public class Credentials {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class SecurityController : Controller
    {
        private readonly IUserService userService;

        public SecurityController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Credentials credentials)
        {
            var token = userService.Authenticate(credentials.UserName, credentials.Password);

            if (token == null)
                return BadRequest(new { message = "Incorrect username or password" });

            return Ok(token);
        }

        [Authorize]
        [HttpPost("check")]
        public void Check()
        {
        }
    }
}