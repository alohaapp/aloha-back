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
        public float X { get; set; }
        [Required]
        public float Y { get; set; }
        
        [ForeignKey(nameof(Worker))]
        public int? WorkerId { get; set; }
        public virtual Worker Worker { get; set; }

        public virtual Floor Floor { get; set; }
    }
}