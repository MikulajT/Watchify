using BLL.Models.BLLModels;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ITvShowService
    {
        /// <summary>
        /// Gets all TV show genres
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BGenre> GetAllTvShowGenres();

        /// <summary>
        /// Gets tv show genres for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public BUserSettings GetUsersTvShowSettings(string userId);

        /// <summary>
        /// Sets tv show genres for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="genres"></param>
        public void ApplyTvShowSettings(string userId, int tvShowsCount, int[] genres);
    }
}
