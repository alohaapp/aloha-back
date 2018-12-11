using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aloha.Model.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        /*[Required]
        public string Salt { get; set; }

        public bool IsAdmin { get; set; }*/

        public virtual Worker Worker { get; set; }
    }
}