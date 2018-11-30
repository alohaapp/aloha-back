using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aloha.Dtos
{
    public class FloorDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int WorkerCount { get; set; }

        public int? ImageId { get; set; }

        public int OfficeId { get; set; }
    }
}