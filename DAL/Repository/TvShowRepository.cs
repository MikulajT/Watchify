using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class TvShowRepository : ITvShowRepository
    {
        public IEnumerable<Genre> GetAllTvShowGenres()
        {
                using (var context = new ApplicationDbContext())
                {
                    var genres = context.Genres.ToList();
                    for (int i = 0; i < genres.Count(); i++)
                    {
                        yield return genres[i];
                    }
                }
        }
    }
}
