using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aloha.Model.Entities
{
    public class Workstation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public float X { get; set; }
        public float Y { get; set; }
        public int FloorId { get; set; }
        public int WorkerId { get; set; }

        public Floor Floor { get; }
        public Worker Worker { get; }
    }
}