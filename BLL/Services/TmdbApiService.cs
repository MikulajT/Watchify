using AutoMapper;
using BLL.ApiModels;
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
        private readonly IMapper _mapper;
        public TmdbApiService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<MovieTvShow> GetPopularMovies(string apiKey, int page = 0)
        {
            TMDbClient client = new TMDbClient(apiKey);
            var movies = client.GetMoviePopularListAsync().Result.Results;
            List<MovieTvShow> moviesTvShows = _mapper.Map<List<SearchMovie>, List<MovieTvShow>>(movies);
            return moviesTvShows;
        }

        public IEnumerable<MovieTvShow> GetPopularTvShows(string apiKey, int page = 0)
        {
            TMDbClient client = new TMDbClient(apiKey);
            var tvShows = client.GetTvShowPopularAsync().Result.Results;
            List<MovieTvShow> moviesTvShows = _mapper.Map<List<SearchTv>, List<MovieTvShow>>(tvShows);
            return moviesTvShows;
        }

        public IEnumerable<Genre> GetGenres(string apiKey)
        {
            TMDbClient client = new TMDbClient(apiKey);
            var genres = client.GetTvGenresAsync().Result;
            return genres;
        }
    }
}
