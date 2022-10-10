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
            ShowDTO showDTO = null;
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


            if (show.Type == ShowType.Movie)
            {
                showDTO = await movieDbService.GetMovie(show.Title.Replace(" ", "+"), Secrets.tmdb_api_key);
            }
            else if (show.Type == ShowType.Series)
            {

                showDTO = await movieDbService.GetSeries(show.Title.Replace(" ", "+"), Secrets.tmdb_api_key);
            }

            if (showDTO == null)
            {
                TempData["Info"] = "Show not found!";
                return RedirectToAction("Index", "Show");
            }

            ViewBag.ShowTitle = showDTO == null ? "No show found!" : showDTO.title;
            ViewBag.Type = showDTO == null ? "" : show.Type.ToString();
            ViewBag.Overview = showDTO.overview;
            ViewBag.Poster = showDTO.poster_path;
            ViewBag.VoteCount = showDTO.vote_count;

            if (showDTO.vote_count == 0)
            {
                ViewBag.Score = "-";
            }
            else
            {
                ViewBag.Score=  showDTO.vote_average;
            }


          

            

            return View();
        }
    }
}
