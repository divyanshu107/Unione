using System.ComponentModel.DataAnnotations;

namespace Unione.Models
{
    public class PhotoModel
    {
        [Key]
        public int Id { get; set; }

        [Required]

        public virtual UserModel User { get; set; }

        [Required]
        public string FilePath { get; set; }

    }

    public class PhotoSubmitModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string FilePath { get; set; }
    }
}
