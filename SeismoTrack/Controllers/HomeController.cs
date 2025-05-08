using Microsoft.AspNetCore.Mvc;
using SeismoTrack.Models;
using SeismoTrack.Services;
using System.Diagnostics;

namespace SeismoTrack.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EarthquakeService _earthquakeService;

        public HomeController(ILogger<HomeController> logger, EarthquakeService earthquakeService)
        {
            _logger = logger;
            _earthquakeService = earthquakeService;
        }

        public async Task<IActionResult> Index()
        {
            var quakes = await _earthquakeService.GetLast3DaysEarthquakesAsync();
            return View(quakes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
