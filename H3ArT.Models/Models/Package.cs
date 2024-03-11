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
        public int packageID { get; set; }

        [Required]
        public string packageName { get; set; }

        public double price { get; set; }

        public int amountPost { get; set; }
        public string description { get; set; }
        public string adminID { get; set; }
        //[ForeignKey("adminID")]
        //public ApplicationUser ApplicationUser { get; set; }



        //public string buyerID { get; set; }
        //[ForeignKey("buyerID")]
        //[ValidateNever]
        //public ApplicationUser applicationUser { get; set; }


    }
}
