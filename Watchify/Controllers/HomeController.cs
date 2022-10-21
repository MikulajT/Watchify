using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Security.Claims;
using Watchify.ViewModels;

namespace Watchify.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITvShowService _tvShowService;
        private readonly IMovieService _movieService;
        private readonly IToastNotification _toastNotification;

        public HomeController(ITvShowService tvShowService, IMovieService movieService, IToastNotification toastNotification)
        {
            _tvShowService = tvShowService;
            _movieService = movieService;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TvShows()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            string loggedUserId = claim.Value;
            ShowModel showModel = new ShowModel()
            {
                Genres = _tvShowService.GetAllTvShowGenres().ToList(),
                UserSettings = _tvShowService.GetUsersTvShowSettings(loggedUserId),
                ShowType = Common.ShowType.TvShow
            };
            return View("TvShowMovieSettings", showModel);
        }

        [HttpGet]
        public IActionResult Movies()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            string loggedUserId = claim.Value;
            ShowModel showModel = new ShowModel()
            {
                Genres = _movieService.GetAllMovieGenres().ToList(),
                UserSettings = _movieService.GetUsersMovieSettings(loggedUserId),
                ShowType = Common.ShowType.Movie
            };
            return View("TvShowMovieSettings", showModel);
        }

        [HttpPost]
        public IActionResult SaveTvShowsSettings(int showsCount, int[] genres)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            string loggedUserId = claim.Value;
            _tvShowService.ApplyTvShowSettings(loggedUserId, showsCount, genres);
            _toastNotification.AddSuccessToastMessage("Tv show settings saved.");
            return RedirectToAction("TvShows");
        }

        [HttpPost]
        public IActionResult SaveMovieSettings(int showsCount, int[] genres)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            string loggedUserId = claim.Value;
            _movieService.ApplyMovieSettings(loggedUserId, showsCount, genres);
            _toastNotification.AddSuccessToastMessage("Movie settings saved.");
            return RedirectToAction("Movies");
        }
    }
}