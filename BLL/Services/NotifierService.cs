using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                string htmlMessage = CreateHtmlMessage(tmdbApiKey);
                _emailService.Send("watchify.com", users[i].Email, "Popular movies and tv shows notification", htmlMessage);
            }
        }

        private string CreateHtmlMessage(string tmdbApiKey)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<ol>");
            foreach (var tvShow in _tmdbApiService.GetPopularTvShows(tmdbApiKey))
            {
                string genres = "";
                if (tvShow.GenreIds.Any())
                {
                    genres = CreateGenresFromIds(tmdbApiKey, tvShow.GenreIds);
                }
                html.Append($@"
                <li>
  	                <ul>
                        <li>Name: {tvShow.Name}</li>
                        <li>Genres: {genres}</li>
                        <li>Rating: {tvShow.VoteAverage}</li>
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
}
