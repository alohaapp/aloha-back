using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aloha.Model.Entities
{
    public class Worker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Url]
        public string PhotoUrl { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Notes { get; set; }

        public User User { get; }
    }
}