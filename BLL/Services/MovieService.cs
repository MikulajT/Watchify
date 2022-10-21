using AutoMapper;
using BLL.Models.BLLModels;
using DAL.Models;
using DAL.Repository;

namespace BLL.Services
{
    public class MovieService : IMovieService
    {
        private IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public IEnumerable<BGenre> GetAllMovieGenres()
        {
            var genres = _movieRepository.GetAllMovieGenres().ToList();
            List<BGenre> mappedGenres = _mapper.Map<List<Genre>, List<BGenre>>(genres);
            return mappedGenres;
        }

        public BUserSettings GetUsersMovieSettings(string userId)
        {
            var movieSettings = _movieRepository.GetUsersMovieSettings(userId);
            BUserSettings mappedMovieSettings = _mapper.Map<UserSettings, BUserSettings>(movieSettings);
            return mappedMovieSettings;
        }

        public void ApplyMovieSettings(string userId, int moviesCount, int[] genres)
        {
            _movieRepository.ApplyMovieSettings(userId, moviesCount, genres);
        }
    }
}
