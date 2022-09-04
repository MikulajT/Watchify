using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ApiModels
{
    public class Movie
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double AverageVote { get; set; }
    }
}
