using Aloha.Dtos;
using Aloha.Mappers;
using Aloha.Model.Entities;

public class UserDtoToUserMapping : IClassMapping<UserDto, User>
{
    public User Map(UserDto userDto)
    {
        return new User()
        {
            UserName = userDto.UserName
        };
    }
}