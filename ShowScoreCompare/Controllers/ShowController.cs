using Microsoft.AspNetCore.Mvc;
using ShowScoreCompare.Data;
using ShowScoreCompare.Models;

namespace ShowScoreCompare.Controllers
{
    public class ShowController : Controller
    {
        private ShowDbContext context;

        public ShowController(ShowDbContext context)
        {
            this.context = context;
        }   

        public IActionResult Index()
        {
            ViewBag.Shows = context.ShowsDB.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult PostedShow(Show show)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index","Show");

            if (show.Title.Length < 3)
            {
                TempData["Info"] = "At least 3 chars required!";

                return RedirectToAction("Index", "Show");
            }
            else
            {
                TempData["Info"] = "";
            }

            ViewBag.ShowTitle = show.Title;
            return View();
        }
    }
}
