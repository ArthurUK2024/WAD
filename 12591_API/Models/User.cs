using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace _12591_API.Modals
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
