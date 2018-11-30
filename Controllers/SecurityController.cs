using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Aloha.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aloha.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SecurityController : Controller
    {
        private readonly ISecurityService securityService;

        public SecurityController(ISecurityService securityService)
        {
            this.securityService = securityService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate([FromBody]CredentialsDto credentials)
        {
            var token = securityService.GenerateJwtToken(credentials.UserName, credentials.Password);

            if (token == null)
            {
                return BadRequest(new { message = "Incorrect username or password" });
            }

            return token;
        }

        [HttpGet("check")]
        public void Check()
        {
        }
    }
}