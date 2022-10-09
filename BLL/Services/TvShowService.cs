using AutoMapper;
using BLL.Models.BLLModels;
using DAL.Models;
using DAL.Repository;

namespace BLL.Services
{
    public class TvShowService : ITvShowService
    {
        private ITvShowRepository _tvShowRepository;
        private readonly IMapper _mapper;

        public TvShowService(ITvShowRepository tvShowRepository, IMapper mapper)
        {
            _tvShowRepository = tvShowRepository;
            _mapper = mapper;
        }

        public IEnumerable<BGenre> GetAllTvShowGenres()
        {
            var genres = _tvShowRepository.GetAllTvShowGenres().ToList();
            List<BGenre> mappedGenres = _mapper.Map<List<Genre>, List<BGenre>>(genres);
            return mappedGenres;
        }

        public BUserSettings GetUsersTvShowSettings(string userId)
        {
            var tvShowSettings = _tvShowRepository.GetUsersTvShowSettings(userId);
            BUserSettings mappedTvShowSettings = _mapper.Map<UserSettings, BUserSettings>(tvShowSettings);
            return mappedTvShowSettings;
        }

        public void ApplyTvShowSettings(string userId, int tvShowsCount, int[] genres)
        {
            _tvShowRepository.ApplyTvShowSettings(userId, tvShowsCount, genres);
        }
    }
}
