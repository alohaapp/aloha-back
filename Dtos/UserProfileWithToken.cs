namespace Aloha.Dtos
{
    public class UserProfileWithToken
    {
        public string UserName { get; set; }

        public string Token { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public int? ImageId { get; set; }
    }
}