using BLL.ApiModels;
using Common;
using DAL.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    public class NotifierService : INotifierService
    {
        private IUsersService _usersService { get; set; }
        private IEmailService _emailService { get; set; }
        private ITmdbApiService _tmdbApiService { get; set; }
        public NotifierService(IUsersService usersService, IEmailService emailService, ITmdbApiService tmdbApiService)
        {
            _usersService = usersService;
            _emailService = emailService;
            _tmdbApiService = tmdbApiService;
        }

        public void NotifyAllUsers(string tmdbApiKey)
        {
            var users = _usersService.GetAllUsers().ToList();
            for (int i = 0; i < users.Count; i++)
            {
                StringBuilder htmlMessage = new StringBuilder();
                if (users[i].TvShowsCount > 0)
                {
                    var tvShows = FetchTvShowsForUser(tmdbApiKey, users[i]);
                    string popularTvShows = CreateHtmlMessage(tmdbApiKey, tvShows, ShowType.TvShow);
                    htmlMessage.Append("<div style=\"width:75%; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif;\">");
                    htmlMessage.Append("<h1>Popular TV shows</h1>");
                    htmlMessage.Append(popularTvShows);
                    htmlMessage.Append("</div>");
                }
                if (users[i].MoviesCount > 0)
                {
                    var movies = FetchMoviesForUser(tmdbApiKey, users[i]);
                    string popularMovies = CreateHtmlMessage(tmdbApiKey, movies, ShowType.Movie);
                    htmlMessage.Append("<div style=\"width:75%; font-family: Roboto,RobotoDraft,Helvetica,Arial,sans-serif;\">");
                    htmlMessage.Append("<h1>Popular Movies</h1>");
                    htmlMessage.Append(popularMovies);
                    htmlMessage.Append("</div>");
                }
                _emailService.Send("watchify.com", users[i].Email, "Popular movies and tv shows notification", htmlMessage.ToString());
            }
        }

        private string CreateHtmlMessage(string tmdbApiKey, List<MovieTvShow> moviesTvShows, ShowType showType)
        {
            StringBuilder html = new StringBuilder();
            string showTypeUrl = showType == ShowType.TvShow ? "tv" : "movie";
            html.Append("<ol style=\"list-style: none; padding: 0;\">");
            for (int i = 0; i < moviesTvShows.Count; i++)
            {
                string genres = "";
                if (moviesTvShows[i].GenreIds.Any())
                {
                    genres = CreateGenresFromIds(tmdbApiKey, moviesTvShows[i].GenreIds, showType);
                }
                html.Append($@"
                <li style=""border-bottom: 5px dotted #b3b3b3; margin-bottom: 10px; padding-bottom: 10px;"">
  	                <ul style=""list-style: none; padding: 0;"">
                        <li><h2 style=""margin: 0;"">{i+1}. {moviesTvShows[i].Name}</h2></li>
                        <li>Genres: {genres}</li>
                        <li>Rating: {moviesTvShows[i].VoteAverage}</li>
                        <li>More info at: https://www.themoviedb.org/{showTypeUrl}/{moviesTvShows[i].Id}</li>
	                </ul> 
                </li>");
            }
            html.Append("</ol>");
            return html.ToString();
        }

        private string CreateGenresFromIds(string tmdbApiKey, List<int> genreIds, ShowType showType)
        {
            StringBuilder result = new StringBuilder();
            Dictionary<int, string> genres;
            if (showType == ShowType.TvShow)
            {
                genres = _tmdbApiService.GetTvShowGenres(tmdbApiKey).ToDictionary(x => x.Id, x => x.Name);
            }
            else
            {
                genres = _tmdbApiService.GetMovieGenres(tmdbApiKey).ToDictionary(x => x.Id, x => x.Name);
            }
            for (int i = 0; i < genreIds.Count; i++)
            {
                result.Append(genres[genreIds[i]] + ", ");
            }
            result.Length = result.Length - 2;
            return result.ToString();
        }

        private List<MovieTvShow> FetchTvShowsForUser(string tmdbApiKey, ApplicationUser user)
        {
            List<MovieTvShow> result = new List<MovieTvShow>(60);
            List<int> userGenres = _usersService.GetUserTvShowGenres(user.Id).ToList();
            int page = 1;
            if (userGenres.Count > 0)
            {
                while (result.Count < user.TvShowsCount)
                {
                    var popularTvShows = _tmdbApiService.GetPopularTvShows(tmdbApiKey, page)
                                            .Where(x => x.GenreIds
                                            .Any(y => userGenres.Contains(y)));
                    result.AddRange(popularTvShows);
                    page++;
                }
            }
            else
            {
                while (result.Count < user.TvShowsCount)
                {
                    var popularTvShows = _tmdbApiService.GetPopularTvShows(tmdbApiKey, page);
                    result.AddRange(popularTvShows);
                    page++;
                }
            }
            return result.Take(user.TvShowsCount).ToList();
        }

        private List<MovieTvShow> FetchMoviesForUser(string tmdbApiKey, ApplicationUser user)
        {
            List<MovieTvShow> result = new List<MovieTvShow>(60);
            List<int> userGenres = _usersService.GetUserMovieGenres(user.Id).ToList();
            int page = 1;
            if (userGenres.Count > 0)
            {
                while (result.Count < user.MoviesCount)
                {
                    var popularMovies = _tmdbApiService.GetPopularMovies(tmdbApiKey, page)
                                            .Where(x => x.GenreIds
                                            .Any(y => userGenres.Contains(y)));
                    result.AddRange(popularMovies);
                    page++;
                }
            }
            else
            {
                while (result.Count < user.MoviesCount)
                {
                    var popularMovies = _tmdbApiService.GetPopularMovies(tmdbApiKey, page);
                    result.AddRange(popularMovies);
                    page++;
                }
            }
            return result.Take(user.MoviesCount).ToList();
        }
    }
}
