using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aloha.Model.Entities
{
    public class Office : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Floor> Floors { get; set; }
    }
}