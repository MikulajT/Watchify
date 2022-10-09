using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.BLLModels
{
    public class BUserSettings
    {
        public IEnumerable<int> GenreIds { get; set; }
        public int ShowsCount { get; set; }
    }
}
