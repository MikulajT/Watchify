using DAL.Models;

namespace DAL.Repository
{
    public class UsersRepository : IUsersRepository
    {
        public ApplicationDbContext _dbContext;

        public UsersRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            var users = _dbContext.Users.ToList();
            for (int i = 0; i < users.Count(); i++)
            {
                yield return users[i];
            }
        }

        public IEnumerable<int> GetUserTvShowGenres(string userId)
        {
            var genres = _dbContext.GenreFilters.Where(x => x.UserId == userId && x.ShowType == Common.ShowType.TvShow)
                .Select(x => x.GenreId)
                .ToList();
            for (int i = 0; i < genres.Count(); i++)
            {
                yield return genres[i];
            }
        }

        public IEnumerable<int> GetUserMovieGenres(string userId)
        {
            var genres = _dbContext.GenreFilters.Where(x => x.UserId == userId && x.ShowType == Common.ShowType.Movie)
                .Select(x => x.GenreId)
                .ToList();
            for (int i = 0; i < genres.Count(); i++)
            {
                yield return genres[i];
            }
        }
    }
}
