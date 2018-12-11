using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

namespace Aloha.Model.Entities
{
    public class File : BaseEntity
    {
        [Required]
        public string MediaType { get; set; }

        [Required]
        public byte[] Data { get; set; }
    }
}