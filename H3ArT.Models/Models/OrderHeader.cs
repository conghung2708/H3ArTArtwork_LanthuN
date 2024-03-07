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
    public class OrderHeader
    {
        public int Id { get; set; }

        [ValidateNever]
        public string applicationUserId { get; set; }
        [ForeignKey("applicationUserId")]
        [ValidateNever]
        public ApplicationUser applicationUser { get; set; }

        public DateTime orderDate { get; set; }
        public double orderTotal { get; set; }

        public string? orderStatus { get; set; }
        public string? paymentStatus { get; set;}

        public DateTime paymentDate { get; set; }

        public string? sessionId { get; set; }
        public string? paymentIntentId { get; set; }

        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Name cannot contain numbers.")]
        public string name { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must start with 0 and have exactly 10 digits.")]
        [MaxLength(10, ErrorMessage = "Phone number cannot exceed 10 characters.")]
        public string phoneNumber { get; set; }
    }
}
