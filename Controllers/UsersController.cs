using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aloha.Dtos;
using Aloha.Mappers;
using Aloha.Model.Contexts;
using Aloha.Model.Entities;
using Aloha.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloha.Controllers
{
    [Obsolete("Users won't be a part of the public API, they'll be created and served in WorkersController.")]
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : Controller
    {
        private readonly AlohaContext dbContext;
        private readonly ISecurityService securityService;
        private readonly IClassMapping<User, UserDto> userToUserDtoMapping;
        private readonly IClassMapping<UserDto, User> userDtoToUserMapping;

        public UsersController(
            AlohaContext dbContext,
            ISecurityService securityService,
            IClassMapping<User, UserDto> userToUserDtoMapping,
            IClassMapping<UserDto, User> userDtoToUserMapping)
        {
            this.dbContext = dbContext;
            this.securityService = securityService;
            this.userToUserDtoMapping = userToUserDtoMapping;
            this.userDtoToUserMapping = userDtoToUserMapping;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public List<UserDto> List()
        {
            return dbContext.Users
                .Select(userToUserDtoMapping.Map)
                .ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<UserDto> GetById(int id)
        {
            User user = dbContext.Users
                .SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return userToUserDtoMapping.Map(user);
        }
    }
}
