using Limupa.Context;
using Limupa.Controllers;
using Limupa.Models;
using Limupa.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class StatisticsController : Controller
    {
        public StatisticsController(AppDbContext context)
        {
            _context = context;
        }

        public AppDbContext _context { get; }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Statistics = _context.Statistics.ToList()
            };
            return View(homeViewModel);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            Statistics statistics = _context.Statistics.FirstOrDefault(x => x.Id == id);
            if (statistics == null) return NotFound();
            return View(statistics);
        }
        [HttpPost]
        public IActionResult Update(Statistics statistics)
        {
            if (!ModelState.IsValid) return View(statistics);
            Statistics existStatistics = _context.Statistics.Find(statistics.Id);
            if (existStatistics == null) return NotFound();
            existStatistics.Name=statistics.Name;
            existStatistics.Number=statistics.Number;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
