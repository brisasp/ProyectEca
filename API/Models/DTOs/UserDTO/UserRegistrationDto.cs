﻿

using DesignAPI.Attributes;
using System.ComponentModel.DataAnnotations;

namespace DesignAPI.Models.DTOs.UserDTO
{
        public class UserRegistrationDto
        {
            [Required(ErrorMessage = "Field required: Name")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Field required: Name")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Field required: Email")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Field required: Password")]
            [PasswordValidation]
            public string Password { get; set; }
            [Required(ErrorMessage = "Field required: Role")]
            public string Role { get; set; }
        }
}
