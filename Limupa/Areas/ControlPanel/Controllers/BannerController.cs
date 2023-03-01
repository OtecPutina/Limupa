using Limupa.Context;
using Limupa.Models;
using Limupa.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class BannerController : Controller
    {
        public BannerController(AppDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _env = environment;
        }

        public AppDbContext _context { get; }
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment _env { get; }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Banners = _context.Banners.ToList()
            };
            return View(homeViewModel);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Banner model)
        {
            if (model.ImageFile.ContentType != "image/png" && model.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "You must upload only png or jpeg files");
                return View();
            }
            if (model.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "You must upload only files under 2mb");
                return View();
            }
            if (!ModelState.IsValid) return NotFound();
            string filename = model.ImageFile.FileName;
            if (filename.Length > 64)
            {
                filename = filename.Substring(filename.Length - 64, 64);
            }
            filename = Guid.NewGuid().ToString() + filename;
            string path = Path.Combine(_env.WebRootPath, "uploads/banner", filename);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                model.ImageFile.CopyTo(stream);
            }
            model.ImageUrl = filename;
            _context.Banners.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            Banner model = _context.Banners.FirstOrDefault(x => x.Id == id);
            if (model is null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(Banner model)
        {
            if (!ModelState.IsValid) return View(model);
            Banner existModel = _context.Banners.Find(model.Id);
            if (existModel is null) return NotFound();
            if (model.ImageFile is not null)
            {
                if (model.ImageFile.ContentType != "image/png" && model.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "You must upload only png or jpeg files");
                    return View();
                }
                if (model.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "You must upload only files under 2mb");
                    return View();
                }
                string deletePath = Path.Combine(_env.WebRootPath, "uploads/banner", existModel.ImageUrl);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }
                string filename = model.ImageFile.FileName;
                if (filename.Length > 64)
                {
                    filename = filename.Substring(filename.Length - 64, 64);
                }
                filename = Guid.NewGuid().ToString() + filename;
                string path = Path.Combine(_env.WebRootPath, "uploads/banner", filename);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    model.ImageFile.CopyTo(stream);
                }
                existModel.ImageUrl = filename;

            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Banner model = _context.Banners.FirstOrDefault(x => x.Id == id);
            if (model is null) return NotFound();
            string deletePath = Path.Combine(_env.WebRootPath, "uploads/models", model.ImageUrl);
            if (System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
            }
            _context.Banners.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
