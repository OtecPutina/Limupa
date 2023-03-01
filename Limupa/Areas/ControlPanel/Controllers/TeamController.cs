using Limupa.Context;
using Limupa.Models;
using Limupa.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Limupa.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    public class TeamController : Controller
    {
        public TeamController(AppDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _env = environment;
        }

        public AppDbContext _context { get; }
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment _env { get; }

        public IActionResult Index()
        {
            ViewBag.Position = _context.Positions;
            HomeViewModel homeVM = new HomeViewModel
            {
                TeamMembers = _context.TeamMembers.Include(x => x.Position).ToList()
            };
            return View(homeVM);
        }
        public IActionResult Create()
        {
            ViewBag.Position = _context.Positions;
            return View();
        }
        [HttpPost]
        public IActionResult Create(TeamMember member)
        {
            ViewBag.Position = _context.Positions;
            if (member.ImageFile.ContentType != "image/png" && member.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "You must upload only png or jpeg files");
                return View();
            }
            if (member.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "You must upload only files under 2mb");
            }
            string filename = member.ImageFile.FileName;
            if (filename.Length > 64)
            {
                filename = filename.Substring(filename.Length - 64, 64);
            }
            filename = Guid.NewGuid().ToString() + filename;
            string path = Path.Combine(_env.WebRootPath, "uploads/member", filename);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                member.ImageFile.CopyTo(stream);
            }
            member.ImageUrl = filename;
            _context.TeamMembers.Add(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Position = _context.Positions;
            TeamMember member = _context.TeamMembers.FirstOrDefault(x => x.Id == id);
            if (member is null) return NotFound();
            return View(member);
        }
        [HttpPost]
        public IActionResult Update(TeamMember member)
        {
            ViewBag.Position = _context.Positions;
            TeamMember existMember = _context.TeamMembers.FirstOrDefault(x => x.Id == member.Id);
            if (member.ImageFile is not null)
            {
                if (member.ImageFile.ContentType != "image/png" && member.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "You must upload only png or jpeg files");
                    return View();
                }
                if (member.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "You must upload only files under 2mb");
                }
                string deletepath = Path.Combine(_env.WebRootPath, "uploads/member", existMember.ImageUrl);
                if (System.IO.File.Exists(deletepath))
                {
                    System.IO.File.Delete(deletepath);
                }
                string filename = member.ImageFile.FileName;
                if (filename.Length > 64)
                {
                    filename = filename.Substring(filename.Length - 64, 64);
                }
                filename = Guid.NewGuid().ToString() + filename;
                string path = Path.Combine(_env.WebRootPath, "uploads/member", filename);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    member.ImageFile.CopyTo(stream);
                }
                existMember.ImageUrl = filename;
            }
            existMember.Name = member.Name;
            existMember.PositionId = member.PositionId;
            existMember.Email= member.Email;
            existMember.FacebookLink = member.FacebookLink;
            existMember.TwitterLink = member.TwitterLink;
            existMember.LinkedinLink = member.LinkedinLink;
            existMember.GooglePlusLink = member.GooglePlusLink;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            TeamMember member = _context.TeamMembers.FirstOrDefault(x => x.Id == id);
            if (member == null) return NotFound();
            string deletepath = Path.Combine(_env.WebRootPath, "uploads/member", member.ImageUrl);
            if (System.IO.File.Exists(deletepath))
            {
                System.IO.File.Delete(deletepath);
            }
            _context.TeamMembers.Remove(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
