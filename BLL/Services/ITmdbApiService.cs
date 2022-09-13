using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

namespace BLL.Services
{
    public interface ITmdbApiService
    {
        public IEnumerable<SearchMovie> GetPopularMovies(string apiKey);
        public IEnumerable<SearchTv> GetPopularTvShows(string apiKey);
    }
}
