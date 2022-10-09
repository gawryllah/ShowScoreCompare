using Microsoft.AspNetCore.Mvc;
using ShowScoreCompare.Data;
using ShowScoreCompare.Models;
using ShowScoreCompare.Services;

namespace ShowScoreCompare.Controllers
{
    public class ShowController : Controller
    {
        private ShowDbContext context;
        private readonly IConfiguration configuration;
        private readonly IMovieDbService movieDbService;

        public ShowController(ShowDbContext context, IConfiguration configuration, IMovieDbService movieDbService)
        {
            this.context = context;
            this.configuration = configuration;
            this.movieDbService = movieDbService;
        }

        public IActionResult Index()
        {
            ViewBag.Shows = context.ShowsDB.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchedShow(Show show)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", "Show");

            if (show.Title.Length < 3)
            {
                TempData["Info"] = "At least 3 chars required!";

                return RedirectToAction("Index", "Show");
            }
            else
            {
                TempData["Info"] = "";
            }

            var obj = await movieDbService.GetMovie(show.Title.Replace(" ", "+"), Secrets.tmdb_api_key);

            ViewBag.ShowTitle = obj != null ? obj.title : "No show found!";
            return View();
        }
    }
}
