using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aloha.Model.Entities
{
    public class Floor : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public File Image { get; set; }

        [Required]
        public virtual Office Office { get; set; }

        public virtual ICollection<Workstation> Workstations { get; set; }
    }
}