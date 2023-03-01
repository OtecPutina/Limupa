using Limupa.Context;
using Limupa.Models;
using Limupa.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class PositionController : Controller
    {
        public PositionController(AppDbContext context)
        {
            _context = context;
        }

        public AppDbContext _context { get; }

        public IActionResult Index()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                Positions = _context.Positions.ToList()
            };
            return View(homeVM);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Position position)
        {
            _context.Positions.Add(position);
            if (ModelState.IsValid) return View();
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            Position position = _context.Positions.FirstOrDefault(x => x.Id == id);
            if (position is null) return NotFound();
            return View(position);
        }
        [HttpPost]
        public IActionResult Update(Position position)
        {
            Position existPosition = _context.Positions.Find(position.Id);
            if (existPosition is null) return NotFound();
            existPosition.Name = position.Name;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Position position = _context.Positions.FirstOrDefault(x => x.Id == id);
            if (position == null) return NotFound();
            _context.Positions.Remove(position);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
