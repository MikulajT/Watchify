using AutoMapper;
using BLL.ApiModels;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

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

        public IEnumerable<Genre> GetTvShowGenres(string apiKey)
        {
            TMDbClient client = new TMDbClient(apiKey);
            var genres = client.GetTvGenresAsync().Result;
            return genres;
        }

        public IEnumerable<Genre> GetMovieGenres(string apiKey)
        {
            TMDbClient client = new TMDbClient(apiKey);
            var genres = client.GetMovieGenresAsync().Result;
            return genres;
        }
    }
}
