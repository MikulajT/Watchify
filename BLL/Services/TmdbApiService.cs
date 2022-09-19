using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

namespace BLL.Services
{
    public class TmdbApiService : ITmdbApiService
    {
        public IEnumerable<SearchMovie> GetPopularMovies(string apiKey)
        {
            TMDbClient client = new TMDbClient(apiKey);
            var movies = client.GetMoviePopularListAsync().Result.Results;
            return movies;
        }

        public IEnumerable<SearchTv> GetPopularTvShows(string apiKey)
        {
            TMDbClient client = new TMDbClient(apiKey);
            var tvShows = client.GetTvShowPopularAsync().Result.Results;
            return tvShows;
        }

        public IEnumerable<Genre> GetGenres(string apiKey)
        {
            TMDbClient client = new TMDbClient(apiKey);
            var genres = client.GetTvGenresAsync().Result;
            return genres;
        }
    }
}
