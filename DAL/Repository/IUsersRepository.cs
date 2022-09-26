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
        /// Gets all users from database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApplicationUser> GetAllUsers();
    }
}
