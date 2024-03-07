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
    public class Artwork
    {
        [Key]
        public int artworkId { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s]*[a-zA-Z][a-zA-Z0-9\s]*$", ErrorMessage = "Title can only contain letters and numbers, and must contain at least one letter.")]
        [MaxLength(100)]
        public string title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MinLength(70, ErrorMessage = "Description must be at least 70 characters.")]
        public string description { get; set; }

        public string artistID { get; set; }

        [ForeignKey("artistID")]
        [ValidateNever]
        public ApplicationUser applicationUser { get; set; }

        [ValidateNever]
        public string? imageUrl { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [RegularExpression(@"^\d*\.?\d+$", ErrorMessage = "Price must be a valid number.")]
        public double price { get; set; }

        [Required(ErrorMessage = "Premium status is required.")]
        public bool isPremium { get; set; }

        public int categoryID { get; set; }

        [ForeignKey("categoryID")]
        [ValidateNever]
        public Category category { get; set; }

        public bool isBought { get; set; }

        public bool reportedConfirm { get; set; }
    }
}
