﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3ArT.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Full name cannot contain numbers.")]
        public string? FullName { get; set; }

        public bool Gender { get; set; }

        [NotMapped]
        public bool Status { get; set; }


        //[RegularExpression(@"^0\d{9}$", ErrorMessage = "phone number must start with 0 and have 10 digits.")]
        //[MaxLength(10, ErrorMessage = "phone number cannot exceed 10 characters.")]
        //public string Phonenumber { get; set; }


        public string? AvatarImage { get; set; }
        [NotMapped]
        public string Role { get; set; }

        public int? AvaiblePost { get; set; }
    }
}
