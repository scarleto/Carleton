using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Carleton.API.Models
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Password { get; set; }

        public bool IsAuthenticated { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime? AuthenticationDate { get; set; }

        public string? AuthenticationString { get; set; }
    }
}
