﻿using AutoMapper;
using BLL.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Search;

namespace BLL
{
    internal class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<SearchTv, MovieTvShow>();
            CreateMap<SearchMovie, MovieTvShow>();
        }
    }
}
