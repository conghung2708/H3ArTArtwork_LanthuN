using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3ArT.Models.Models
{
    public class ReportArtwork
    {
        [Key]
        public int reportArtworkID { get; set; }
        public int artworkID { get; set; }
        public string reporterUserID { get; set; }
    }
}