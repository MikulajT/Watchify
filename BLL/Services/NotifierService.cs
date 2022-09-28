using AutoMapper;
using BLL.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

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
                if (users[i].SendTvShowNotifications)
                {
                    var tvShows = _tmdbApiService.GetPopularTvShows(tmdbApiKey).ToList();
                    string popularTvShows = CreateHtmlMessage(tmdbApiKey, tvShows, ShowType.TvShow);
                    htmlMessage.Append("<h1>Popular Movies</h1>");
                    htmlMessage.Append(popularTvShows);
                }
                if (users[i].SendMovieNotifications)
                {
                    var movies = _tmdbApiService.GetPopularTvShows(tmdbApiKey).ToList();
                    string popularMovies = CreateHtmlMessage(tmdbApiKey, movies, ShowType.Movie);
                    htmlMessage.Append("<h1>Popular TV shows</h1>");
                    htmlMessage.Append(popularMovies);
                }
                _emailService.Send("watchify.com", users[i].Email, "Popular movies and tv shows notification", htmlMessage.ToString());
            }
        }

        private string CreateHtmlMessage(string tmdbApiKey, List<MovieTvShow> moviesTvShows, ShowType showType)
        {
            string showTypeUrl = "";
            if (showType == ShowType.TvShow)
            {
                showTypeUrl += "tv";
            }
            else if (showType == ShowType.Movie)
            {
                showTypeUrl += "movie";
            }
            StringBuilder html = new StringBuilder();
            html.Append("<ol>");
            foreach (var movieTvShow in moviesTvShows)
            {
                string genres = "";
                if (movieTvShow.GenreIds.Any())
                {
                    genres = CreateGenresFromIds(tmdbApiKey, movieTvShow.GenreIds);
                }
                html.Append($@"
                <li>
  	                <ul>
                        <li>Name: {movieTvShow.Name}</li>
                        <li>Genres: {genres}</li>
                        <li>Rating: {movieTvShow.VoteAverage}</li>
                        <li>More info at: https://www.themoviedb.org/{showTypeUrl}/{movieTvShow.Id}</li>
	                </ul> 
                </li>");
            }
            html.Append("</ol>");
            return html.ToString();
        }

        private string CreateGenresFromIds(string tmdbApiKey, List<int> genreIds)
        {
            StringBuilder result = new StringBuilder();
            Dictionary<int, string> genres = _tmdbApiService.GetGenres(tmdbApiKey).ToDictionary(x => x.Id, x => x.Name);
            for (int i = 0; i < genreIds.Count; i++)
            {
                result.Append(genres[genreIds[i]] + ", ");
            }
            result.Length = result.Length - 2;
            return result.ToString();
        }
    }

    enum ShowType {
        TvShow,
        Movie
    }
}
