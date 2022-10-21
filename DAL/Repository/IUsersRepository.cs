using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IUsersRepository
    {
        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApplicationUser> GetAllUsers();

        /// <summary>
        /// Gets users tv show genres
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetUserTvShowGenres(string userId);

        /// <summary>
        /// Gets users movie genres
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetUserMovieGenres(string userId);
    }
}
