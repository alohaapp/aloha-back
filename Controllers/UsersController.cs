using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aloha.Dtos;
using Aloha.Mappers;
using Aloha.Model.Entities;
using Aloha.Models.Contexts;
using Aloha.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloha.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : Controller
    {
        private readonly AlohaContext alohaContext;
        private readonly IUserService userService;
        private readonly IClassMapping<User, UserDto> userToUserDtoMapping;
        private readonly IClassMapping<UserDto, User> userDtoToUserMapping;

        public UsersController(
            AlohaContext alohaContext,
            IUserService userService,
            IClassMapping<User, UserDto> userToUserDtoMapping,
            IClassMapping<UserDto, User> userDtoToUserMapping)
        {
            this.alohaContext = alohaContext;
            this.userService = userService;
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

            user.PasswordHash = userService.HashPassword("test");

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
