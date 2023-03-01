using Limupa.Context;
using Limupa.Models;
using Limupa.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class SettingController : Controller
    {
        public SettingController(AppDbContext context)
        {
            _context = context;
        }

        public AppDbContext _context { get; }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Settings = _context.Settings.ToList()
            };
            return View(homeViewModel);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            Setting setting = _context.Settings.FirstOrDefault(x => x.Id == id);
            if (setting == null) return NotFound();
            return View(setting);
        }
        [HttpPost]
        public IActionResult Update(Setting setting)
        {
            Setting existSetting = _context.Settings.Find(setting.Id);
            if (existSetting == null) return NotFound();
            existSetting.Value = setting.Value;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}