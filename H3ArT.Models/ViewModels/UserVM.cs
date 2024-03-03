using H3ArT.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3ArT.Models.ViewModels
{
    public class UserVM
    {
        public ApplicationUser artist { get; set; }

        public IEnumerable<Artwork> artworkList { get; set; }

    }
}