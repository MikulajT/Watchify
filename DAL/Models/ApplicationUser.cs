﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public bool SendTvShowNotifications { get; set; }
        [Required]
        public bool SendMovieNotifications { get; set; }
    }
}