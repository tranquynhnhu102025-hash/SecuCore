using System.ComponentModel.DataAnnotations;

namespace SecuCore.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(256)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, MaxLength(20)]
        public string Role { get; set; } // SystemAdmin hoặc Viewer

        // Navigation property
        // public ICollection<WebsiteTarget> WebsiteTargets { get; set; }
    }
}