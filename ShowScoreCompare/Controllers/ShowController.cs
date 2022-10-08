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
            ViewBag.ShowTitle = show.Title;
            return View();
        }
    }
}
