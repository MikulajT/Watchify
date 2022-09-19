﻿using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UsersService : IUsersService
    {
        private IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public IEnumerable<IdentityUser> GetAllUsers()
        {
            return _usersRepository.GetAllUsers();
        }
    }
}
