using System.ComponentModel.DataAnnotations;

namespace Aloha.Dtos
{
    public class WorkerDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhotoUrl { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Notes { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int? PhotoId { get; set; }

        public int? WorkstationId { get; set; }
    }
}