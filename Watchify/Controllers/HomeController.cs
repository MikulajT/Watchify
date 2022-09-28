using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Watchify.Models;
using static Org.BouncyCastle.Math.EC.ECCurve;

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
            var genres = _tvShowService.GetAllTvShowGenres().ToList();
            return View(genres);
        }

        [HttpGet]
        public IActionResult Movies()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveTvShowsSettings(int tvShowsCount, int[] genres)
        {
            //TODO
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