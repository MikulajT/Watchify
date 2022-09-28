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
    }
}
