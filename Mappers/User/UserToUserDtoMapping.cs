using Aloha.Dtos;
using Aloha.Model.Entities;

public class UserToUserDtoMapping : IClassMapping<User, UserDto>
{
    public UserDto Map(User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            UserName = user.UserName,
            WorkerId = user.Worker == null ? null : (int?)user.Worker.Id
        };
    }
}