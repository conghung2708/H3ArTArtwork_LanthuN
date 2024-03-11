using H3ArT.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3ArT.Models.ViewModels
{
    public class PackagePaymentVM
    {
        public int packageId { get; set; }
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [ValidateNever]
        public Package package { get; set; }
        public OrderHeader orderHeader { get; set; }
    }
}
