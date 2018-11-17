using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aloha.Model.Entities;

namespace Aloha.Dtos
{
    public class FloorDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        public int OfficeId { get; set; }

        public ICollection<Workstation> Workstations { get; set; }
    }
}