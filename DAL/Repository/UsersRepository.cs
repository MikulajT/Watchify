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
        public IEnumerable<IdentityUser> GetAllUsers()
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
    }
}
