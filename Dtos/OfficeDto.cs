using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aloha.Model.Entities;

namespace Aloha.Dtos
{
    public class OfficeDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}