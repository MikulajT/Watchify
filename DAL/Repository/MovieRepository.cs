using Common;
using DAL.Models;

namespace DAL.Repository
{
    public class MovieRepository : IMovieRepository
    {

        public ApplicationDbContext _dbContext;

        public MovieRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Genre> GetAllMovieGenres()
        {
            var genres = _dbContext.Genres.Where(x => x.MovieGenre).ToList();
            for (int i = 0; i < genres.Count(); i++)
            {
                yield return genres[i];
            }
        }

        public UserSettings GetUsersMovieSettings(string userId)
        {
            var genresIds = _dbContext.GenreFilters.Where(x => x.UserId == userId && x.ShowType == ShowType.Movie)
                .Select(x => x.GenreId)
                .ToList();

            #pragma warning disable CS8602 // User will be logged at this point 
            var moviesCount = _dbContext.Users.SingleOrDefault(x => x.Id == userId).MoviesCount;
            return new UserSettings()
            {
                GenreIds = genresIds,
                ShowsCount = moviesCount
            };
        }

        public void ApplyMovieSettings(string userId, int moviesCount, int[] genres)
        {
            List<GenreFilter> genreFilters = new List<GenreFilter>(19);
            List<GenreFilter> previousFilters = _dbContext.GenreFilters.Where(x => x.UserId == userId &&
                                                            x.ShowType == ShowType.Movie).ToList();
            _dbContext.GenreFilters.RemoveRange(previousFilters);
            var user = _dbContext.Users.Single(x => x.Id == userId);
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
            _dbContext.AddRange(genreFilters);
            _dbContext.SaveChanges();
        }
    }
}
