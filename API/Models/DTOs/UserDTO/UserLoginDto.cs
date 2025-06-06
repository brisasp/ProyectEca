﻿

using System.ComponentModel.DataAnnotations;

namespace DesignAPI.Models.DTOs.UserDTO
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Field required: UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Field required: Password")]
        public string Password { get; set; }
    }
}
