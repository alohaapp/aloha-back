using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Aloha.Dtos;
using Aloha.Model.Contexts;
using Aloha.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloha.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SecurityController : Controller
    {
        private readonly AlohaContext alohaDbContext;
        private readonly ISecurityService securityService;

        public SecurityController(AlohaContext alohaDbContext, ISecurityService securityService)
        {
            this.alohaDbContext = alohaDbContext;
            this.securityService = securityService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ActionResult<UserProfileWithToken> Authenticate([FromBody]CredentialsDto credentials)
        {
            var token = securityService.GenerateJwtToken(credentials.UserName, credentials.Password);

            if (token == null)
            {
                return BadRequest(new { message = "Incorrect username or password" });
            }

            var user = alohaDbContext.Users
                .Single(u => u.UserName == credentials.UserName);

            var worker = alohaDbContext.Workers
                .Include(w => w.Photo)
                .Single(w => w.UserId == user.Id);

            var response = new UserProfileWithToken()
            {
                UserName = user.UserName,
                Token = token,
                Name = worker.Name,
                SurName = worker.Surname,
                ImageId = worker.Photo?.Id
            };

            return new ActionResult<UserProfileWithToken>(response);
        }

        [HttpGet("check")]
        public void Check()
        {
        }
    }
}