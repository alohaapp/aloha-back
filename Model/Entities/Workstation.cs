using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aloha.Model.Entities
{
    public class Workstation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required]
        public Decimal X { get; set; }

        [Required]
        public Decimal Y { get; set; }

        [ForeignKey(nameof(Worker))]
        public int? WorkerId { get; set; }

        public virtual Worker Worker { get; set; }

        [Required]
        public virtual Floor Floor { get; set; }
    }
}