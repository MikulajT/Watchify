using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

namespace BLL.Services
{
    public interface ITmdbApiService
    {
        public IEnumerable<SearchMovie> GetPopularMovies(string apiKey, int page = 0);
        public IEnumerable<SearchTv> GetPopularTvShows(string apiKey, int page = 0);
        public IEnumerable<Genre> GetGenres(string apiKey);
    }
}
