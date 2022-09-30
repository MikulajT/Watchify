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
        public IEnumerable<Genre> GetAllGenres()
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

        public void ApplyTvShowSettings(string userId, int tvShowsCount, int[] genres)
        {
            using (var context = new ApplicationDbContext())
            {
                List<GenreFilters> genreFilters = new List<GenreFilters>(16);
                for (int i = 0; i < genres.Length; i++)
                {
                    var user = context.Users.Single(x => x.Id == userId);
                    user.TvShowsCount = tvShowsCount;
                    List<GenreFilters> previousFilters = context.GenreFilters.Where(x => x.UserId == userId && 
                                                                                    x.ShowType == ShowType.TvShow).ToList();
                    context.GenreFilters.RemoveRange(previousFilters);
                    GenreFilters genreFilter = new GenreFilters()
                    {
                        UserId = userId,
                        GenreId = genres[i],
                        ShowType = ShowType.TvShow
                    };
                    genreFilters.Add(genreFilter);
                }
                context.AddRange(genreFilters);
                context.SaveChanges();
            }
        }
    }
}
