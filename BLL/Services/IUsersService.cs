﻿using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IUsersService
    {
        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApplicationUser> GetAllUsers();

        /// <summary>
        /// Gets user genres
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetUserGenres(string userId);
    }
}
