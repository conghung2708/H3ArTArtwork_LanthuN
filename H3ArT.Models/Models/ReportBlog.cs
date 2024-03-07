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
    public class ReportBlog
    {
        [Key]
        public int reportBlogID { get; set; }

        public int blogID { get; set; }
        [ForeignKey("blogID")]
        [ValidateNever]
        public Blog blog { get; set; }

        public string reporterUserID { get; set; }
        [ForeignKey("reporterUserID")]
        [ValidateNever]
        public ApplicationUser applicationUser { get; set; }
    }
}