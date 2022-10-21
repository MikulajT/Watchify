using Common;
using DAL.Models;

namespace DAL.Repository
{
    public class MovieRepository : IMovieRepository
    {
        public IEnumerable<Genre> GetAllMovieGenres()
        {
            using (var context = new ApplicationDbContext())
            {
                var genres = context.Genres.Where(x => x.MovieGenre).ToList();
                for (int i = 0; i < genres.Count(); i++)
                {
                    yield return genres[i];
                }
            }
        }

        public UserSettings GetUsersMovieSettings(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var genresIds = context.GenreFilters.Where(x => x.UserId == userId && x.ShowType == ShowType.Movie)
                    .Select(x => x.GenreId)
                    .ToList();

#pragma warning disable CS8602 // User will be logged at this point 
                var moviesCount = context.Users.SingleOrDefault(x => x.Id == userId).MoviesCount;
                return new UserSettings()
                {
                    GenreIds = genresIds,
                    ShowsCount = moviesCount
                };
            }
        }

        public void ApplyMovieSettings(string userId, int moviesCount, int[] genres)
        {
            using (var context = new ApplicationDbContext())
            {
                List<GenreFilter> genreFilters = new List<GenreFilter>(19);
                List<GenreFilter> previousFilters = context.GenreFilters.Where(x => x.UserId == userId &&
                                                               x.ShowType == ShowType.Movie).ToList();
                context.GenreFilters.RemoveRange(previousFilters);
                var user = context.Users.Single(x => x.Id == userId);
                user.MoviesCount = moviesCount;
                for (int i = 0; i < genres.Length; i++)
                {
                    GenreFilter genreFilter = new GenreFilter()
                    {
                        UserId = userId,
                        GenreId = genres[i],
                        ShowType = ShowType.Movie
                    };
                    genreFilters.Add(genreFilter);
                }
                context.AddRange(genreFilters);
                context.SaveChanges();
            }
        }
    }
}
