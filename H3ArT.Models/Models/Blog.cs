using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3ArT.Models.Models
{
    public class Blog
    {
        [Key]
        public int blogID { get; set; }

        public string creatorID { get; set; }
        [ForeignKey("creatorID")]
        [ValidateNever]
        public ApplicationUser applicationUser { get; set; }

        public string title { get; set; }
        [StringLength(100, MinimumLength = 100, ErrorMessage = "Description must be at least 100 characters.")]
        public string description { get; set; }
        public DateTime createAt { get; set; }
        [ValidateNever]
        public string imageUrl { get; set; }
    }
}
