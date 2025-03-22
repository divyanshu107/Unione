using System.ComponentModel.DataAnnotations;

namespace Unione.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(254)]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
