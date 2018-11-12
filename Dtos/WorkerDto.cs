using System.ComponentModel.DataAnnotations;

namespace Aloha.Dtos
{
    public class WorkerDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Surname { get; set; }

        [Url]
        public string PhotoUrl { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Notes { get; set; }

        public int? UserId { get; set; }

        public int? WorkstationId { get; set; }
    }
}