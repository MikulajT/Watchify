using BLL.Models.BLLModels;

namespace BLL.Services
{
    public interface IMovieService
    {
        /// <summary>
        /// Gets all Movie genres
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BGenre> GetAllMovieGenres();

        /// <summary>
        /// Gets movie genres for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public BUserSettings GetUsersMovieSettings(string userId);

        /// <summary>
        /// Sets movie genres for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="genres"></param>
        public void ApplyMovieSettings(string userId, int moviesCount, int[] genres);
    }
}
