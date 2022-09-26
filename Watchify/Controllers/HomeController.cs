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
        public HomeController(INotifierService notifierService, IConfiguration config)
        {
            _notifierService = notifierService;
            _config = config;
        }

        public IActionResult Index()
        {        
            //_notifierService.NotifyAllUsers(_config["TmdbApiKey"]);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}