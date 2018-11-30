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
    [Route("api/v1/[controller]")]
    public class UsersController : Controller
    {
        private readonly AlohaContext alohaContext;
        private readonly ISecurityService securityService;
        private readonly IClassMapping<User, UserDto> userToUserDtoMapping;
        private readonly IClassMapping<UserDto, User> userDtoToUserMapping;

        public UsersController(
            AlohaContext alohaContext,
            ISecurityService securityService,
            IClassMapping<User, UserDto> userToUserDtoMapping,
            IClassMapping<UserDto, User> userDtoToUserMapping)
        {
            this.alohaContext = alohaContext;
            this.securityService = securityService;
            this.userToUserDtoMapping = userToUserDtoMapping;
            this.userDtoToUserMapping = userDtoToUserMapping;
        }

        [HttpGet]
        public List<UserDto> List()
        {
            return alohaContext.Users
                .Select(userToUserDtoMapping.Map)
                .ToList();
        }

        [HttpGet("{id}")]
        public UserDto GetById(int id)
        {
            User user = alohaContext.Users.Find(id);

            return user == null
                ? null
                : userToUserDtoMapping.Map(user);
        }

        [HttpPost]
        public UserDto Add([FromBody]UserDto userDto)
        {
            User user = userDtoToUserMapping.Map(userDto);

            user.PasswordHash = securityService.HashPassword("test");

            alohaContext.Users.Add(user);

            alohaContext.SaveChanges();

            return userToUserDtoMapping.Map(user);
        }

        [HttpDelete("{id}")]
        public void Remove(int id)
        {
            User user = alohaContext.Users.Find(id);

            alohaContext.Users.Remove(user);

            alohaContext.SaveChanges();
        }
    }
}
