using DAL.Models;

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

        public IEnumerable<int> GetUserTvShowGenres(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var genres = context.GenreFilters.Where(x => x.UserId == userId && x.ShowType == Common.ShowType.TvShow)
                    .Select(x => x.GenreId)
                    .ToList();
                for (int i = 0; i < genres.Count(); i++)
                {
                    yield return genres[i];
                }
            }
        }

        public IEnumerable<int> GetUserMovieGenres(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var genres = context.GenreFilters.Where(x => x.UserId == userId && x.ShowType == Common.ShowType.Movie)
                    .Select(x => x.GenreId)
                    .ToList();
                for (int i = 0; i < genres.Count(); i++)
                {
                    yield return genres[i];
                }
            }
        }
    }
}
