using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Watchify.Models;
using Watchify.ViewModels;

namespace Watchify.Controllers
{
    public class HomeController : Controller
    {
        private readonly INotifierService _notifierService;
        private readonly IConfiguration _config;
        private readonly ITvShowService _tvShowService;
        public HomeController(INotifierService notifierService, IConfiguration config, ITvShowService tvShowService)
        {
            _notifierService = notifierService;
            _config = config;
            _tvShowService = tvShowService;
        }

        [HttpGet]
        public IActionResult Index()
        {        
            //_notifierService.NotifyAllUsers(_config["TmdbApiKey"]);
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
            ShowModel showModel = new ShowModel()
            {
                Genres = _tvShowService.GetAllTvShowGenres().ToList(),
                ShowType = Common.ShowType.Movie
            };
            return View("TvShowMovieSettings", showModel);
        }

        [HttpPost]
        public IActionResult SaveTvShowsSettings(int tvShowsCount, int[] genres)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            string loggedUserId = claim.Value;
            _tvShowService.ApplyTvShowSettings(loggedUserId, tvShowsCount, genres);
            return RedirectToAction("TvShows");
        }

        [HttpPost]
        public IActionResult SaveMoviesettings()
        {
            //TODO
            return RedirectToAction("Movies");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}