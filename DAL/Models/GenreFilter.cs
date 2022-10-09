using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DAL.Models
{
    public class GenreFilter
    {
        public string UserId { get; set; }
        public int GenreId { get; set; }
        public ShowType ShowType { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }
    }


}
