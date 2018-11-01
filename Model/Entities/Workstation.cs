using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aloha.Model.Entities
{
    public class Workstation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }
        public float X { get; set; }
        public float Y { get; set; }

        public int FloorId { get; set; }
        public Floor Floor { get; }
    }
}