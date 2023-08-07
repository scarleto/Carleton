using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Carleton.API.Helpers;

namespace Carleton.API.Models
{
    public class UserForUpdateDto
    {

        [Required(ErrorMessage = "You should provide a First Name.")]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "You should provide a Last Name.")]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "You should provide an Email Address.")]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "You should provide a Password.")]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;


    }
}
