using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ApiModels
{
    /// <summary>
    /// Represents movie or TV show (TMDP API provides property 'name' for TV show and 'title' for movie 🤦)
    /// </summary>
    public class MovieTvShow
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public double VoteAverage { get; set; }
        public List<int> GenreIds { get; set; }
    }
}
