using DAL.Models;

namespace DAL.Repository
{
    public interface IMovieRepository
    {
        /// <summary>
        /// Gets all movie genres
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Genre> GetAllMovieGenres();

        /// <summary>
        /// Gets movie genres for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserSettings GetUsersMovieSettings(string userId);

        /// <summary>
        /// Sets movie genres for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="genres"></param>
        public void ApplyMovieSettings(string userId, int moviesCount, int[] genres);
    }
}
