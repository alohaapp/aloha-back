using System.ComponentModel.DataAnnotations;

namespace Aloha.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public int? WorkerId { get; set; }
    }
}