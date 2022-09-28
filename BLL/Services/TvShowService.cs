using AutoMapper;
using BLL.ApiModels;
using BLL.Models.BLLModels;
using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Search;

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
            var genres = _tvShowRepository.GetAllGenres().ToList();
            List<BGenre> mappedGenres = _mapper.Map<List<Genre>, List<BGenre>>(genres);
            return mappedGenres;
        }

        public void ApplyTvShowSettings(string userId, int tvShowsCount, int[] genres)
        {
            _tvShowRepository.ApplyTvShowSettings(userId, tvShowsCount, genres);
        }
    }
}
