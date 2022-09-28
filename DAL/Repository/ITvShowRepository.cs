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
    }
}
