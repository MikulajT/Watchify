using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface ITvShowRepository
    {
        /// <summary>
        /// Gets all tv show genres
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Genre> GetAllTvShowGenres();

        /// <summary>
        /// Gets tv show genres for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserSettings GetUsersTvShowSettings(string userId);

        /// <summary>
        /// Sets tv show genres for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="genres"></param>
        public void ApplyTvShowSettings(string userId, int tvShowsCount, int[] genres);
    }
}
