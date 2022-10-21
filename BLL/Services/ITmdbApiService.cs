using BLL.ApiModels;
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
        public IEnumerable<MovieTvShow> GetPopularMovies(string apiKey, int page = 0);
        public IEnumerable<MovieTvShow> GetPopularTvShows(string apiKey, int page = 0);
        public IEnumerable<Genre> GetTvShowGenres(string apiKey);
        public IEnumerable<Genre> GetMovieGenres(string apiKey);
    }
}
