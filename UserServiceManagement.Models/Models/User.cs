using System.ComponentModel.DataAnnotations;

namespace UserServiceManagement.Models.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? ProfilePictureUrl { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
