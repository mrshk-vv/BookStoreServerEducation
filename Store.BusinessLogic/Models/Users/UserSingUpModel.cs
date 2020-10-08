﻿using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Users
{
    public class UserSingUpModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
