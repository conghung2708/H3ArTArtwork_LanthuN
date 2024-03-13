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


        public double Price { get; set; }

        public int AmountPost { get; set; }
        public string Description { get; set; }
        public string AdminID { get; set; }
        //[ForeignKey("adminID")]
        //public ApplicationUser ApplicationUser { get; set; }



        //public string buyerID { get; set; }
        //[ForeignKey("buyerID")]
        //[ValidateNever]
        //public ApplicationUser applicationUser { get; set; }



    }
}
