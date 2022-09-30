using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UsersRepository : IUsersRepository
    {
        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                var users = context.Users.ToList();
                for (int i = 0; i < users.Count(); i++)
                {
                    yield return users[i];
                }
            }
        }

        public IEnumerable<int> GetUserGenres(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var genres = context.GenreFilters.Where(x => x.UserId == userId).Select(x => x.GenreId).ToList();
                for (int i = 0; i < genres.Count(); i++)
                {
                    yield return genres[i];
                }
            }
        }
    }
}
