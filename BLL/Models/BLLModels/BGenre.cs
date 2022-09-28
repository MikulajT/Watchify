using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.BLLModels
{
    public class BGenre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
