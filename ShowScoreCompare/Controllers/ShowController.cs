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
        private readonly ITMDB_Service tmdbService; //tmdb
        private readonly IIMDB_Service imdbService; //imdb

        public ShowController(ShowDbContext context, IConfiguration configuration, ITMDB_Service tmdbService, IIMDB_Service imdbService)
        {
            this.context = context;
            this.configuration = configuration;
            this.tmdbService = tmdbService;
            this.imdbService = imdbService;
        }

        public IActionResult Index()
        {
            ViewBag.Shows = context.ShowsDB.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchedShow(Show show)
        {
            ShowDTO imdbDTO = null; //main dto
            ShowDTO tmdbDTO = null;

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
           
                imdbDTO = await imdbService.GetMovie(show.Title, Secrets.imdb_api_key);
                tmdbDTO = await tmdbService.GetMovie(imdbDTO.title, Secrets.tmdb_api_key);
            }
            else if (show.Type == ShowType.Series)
            {
               

                imdbDTO = await imdbService.GetSeries(show.Title, Secrets.imdb_api_key);
                tmdbDTO = await tmdbService.GetSeries(imdbDTO.title, Secrets.tmdb_api_key);
            }

            if (imdbDTO == null)
            {
                TempData["Info"] = "Show not found!";
                return RedirectToAction("Index", "Show");
            }

            ViewBag.ShowTitle = imdbDTO == null ? "No show found!" : imdbDTO.title;
            ViewBag.Type = show?.Type.ToString();
            ViewBag.Overview = imdbDTO?.overview;
            ViewBag.Poster = imdbDTO?.poster_path;
            ViewBag.ImdbVc = imdbDTO?.vote_count;
            ViewBag.TmdbVc = tmdbDTO?.vote_count;

            if (imdbDTO?.vote_count == 0)
            {
                ViewBag.ImdbScore = "-";
                ViewBag.TmdbScore = "-";
            }
            else
            {
                ViewBag.ImdbScore =  imdbDTO?.vote_average;
                ViewBag.TmdbScore = tmdbDTO?.vote_average;
            }




            return View();
        }
    }
}
