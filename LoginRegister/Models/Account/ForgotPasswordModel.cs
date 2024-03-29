﻿using System.ComponentModel.DataAnnotations;


namespace Oblivion.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
