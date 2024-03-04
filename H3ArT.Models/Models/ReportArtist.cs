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
    public class ReportArtist
    {
        [Key]
        public int reportArtistID { get; set; }
        public string artistID { get; set; }
        [ForeignKey("artistID")]
        [ValidateNever]
        public ApplicationUser artist {  get; set; }

        public string reporterUserID { get; set; }
        [ForeignKey("reporterUserID")]
        [ValidateNever]
        public ApplicationUser reporter {  get; set; }
    }
}