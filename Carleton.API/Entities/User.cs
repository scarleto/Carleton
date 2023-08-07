using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Carleton.API.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public bool IsAuthenticated { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime? AuthenticationDate { get; set; }

        [Required]
        [MaxLength(6)]
        public string AuthenticationString { get; set; } = string.Empty;

        public User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
