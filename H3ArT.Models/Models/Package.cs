using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3ArT.Models.Models
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }

        [Required]
        public string PackageName { get; set; }

        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double Price { get; set; }

        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Amount artworks must be greater than 0.")]
        public int AmountPost { get; set; }

        [Required]
        public string Description { get; set; }
        public string AdminID { get; set; }
    }
}
