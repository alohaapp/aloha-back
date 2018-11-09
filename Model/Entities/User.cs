using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aloha.Model.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        [Required]
        public string Username { get; set; }
        //public string Password { get; set; } // Insecure D:
        public bool IsAdmin { get; set; } // 👀

        [ForeignKey(nameof(Worker))]
        public int? WorkerId { get; set; }
        public virtual Worker Worker { get; set; }
    }
}