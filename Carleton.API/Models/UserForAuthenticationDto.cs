﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Carleton.API.Helpers;

namespace Carleton.API.Models
{
    public class UserForAuthenticationDto
    {
        private string _x = string.Empty;

        [Required(ErrorMessage = "You should provide a First Name.")]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "You should provide a Last Name.")]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "You should provide a Password.")]
        [MaxLength(20)]
        public string Password { get; set; } = string.Empty;


    }
}
