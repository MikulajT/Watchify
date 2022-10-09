using Common;
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
                    var genres = context.Genres.Where(x => x.TvShowGenre).ToList();
                    for (int i = 0; i < genres.Count(); i++)
                    {
                        yield return genres[i];
                    }
                }
        }

        public UserSettings GetUsersTvShowSettings(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var genresIds = context.GenreFilters.Where(x => x.UserId == userId && x.Genre.TvShowGenre)
                    .Select(x => x.GenreId)
                    .ToList();

                #pragma warning disable CS8602 // User will be logged at this point 
                var tvShowsCount = context.Users.SingleOrDefault(x => x.Id == userId).TvShowsCount;
                return new UserSettings()
                {
                    GenreIds = genresIds,
                    ShowsCount = tvShowsCount
                };
            }
        }

        public void ApplyTvShowSettings(string userId, int tvShowsCount, int[] genres)
        {
            using (var context = new ApplicationDbContext())
            {
                List<GenreFilter> genreFilters = new List<GenreFilter>(16);
                for (int i = 0; i < genres.Length; i++)
                {
                    var user = context.Users.Single(x => x.Id == userId);
                    user.TvShowsCount = tvShowsCount;
                    List<GenreFilter> previousFilters = context.GenreFilters.Where(x => x.UserId == userId && 
                                                                                    x.ShowType == ShowType.TvShow).ToList();
                    context.GenreFilters.RemoveRange(previousFilters);
                    GenreFilter genreFilter = new GenreFilter()
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
