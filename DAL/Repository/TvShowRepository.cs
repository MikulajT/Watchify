using Common;
using DAL.Models;

namespace DAL.Repository
{
    public class TvShowRepository : ITvShowRepository
    {
        public ApplicationDbContext _dbContext;

        public TvShowRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Genre> GetAllTvShowGenres()
        {
            var genres = _dbContext.Genres.Where(x => x.TvShowGenre).ToList();
            for (int i = 0; i < genres.Count(); i++)
            {
                yield return genres[i];
            }
        }

        public UserSettings GetUsersTvShowSettings(string userId)
        {
            var genresIds = _dbContext.GenreFilters.Where(x => x.UserId == userId && x.ShowType == ShowType.TvShow)
                .Select(x => x.GenreId)
                .ToList();

            #pragma warning disable CS8602 // User will be logged at this point 
            var tvShowsCount = _dbContext.Users.SingleOrDefault(x => x.Id == userId).TvShowsCount;
            return new UserSettings()
            {
                GenreIds = genresIds,
                ShowsCount = tvShowsCount
            };
        }

        public void ApplyTvShowSettings(string userId, int tvShowsCount, int[] genres)
        {
            List<GenreFilter> genreFilters = new List<GenreFilter>(16);
            List<GenreFilter> previousFilters = _dbContext.GenreFilters.Where(x => x.UserId == userId &&
                                                            x.ShowType == ShowType.TvShow).ToList();
            _dbContext.GenreFilters.RemoveRange(previousFilters);
            var user = _dbContext.Users.Single(x => x.Id == userId);
            user.TvShowsCount = tvShowsCount;
            for (int i = 0; i < genres.Length; i++)
            {
                GenreFilter genreFilter = new GenreFilter()
                {
                    UserId = userId,
                    GenreId = genres[i],
                    ShowType = ShowType.TvShow
                };
                genreFilters.Add(genreFilter);
            }
            _dbContext.AddRange(genreFilters);
            _dbContext.SaveChanges();
        }
    }
}
