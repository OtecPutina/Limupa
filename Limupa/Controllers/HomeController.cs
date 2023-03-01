using Limupa.Context;
using Limupa.Models;
using Limupa.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Limupa.Controllers
{
    public class HomeController : Controller
    {
        public AppDbContext _context { get; }

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Sliders = _context.Sliders.ToList(),
                Banners= _context.Banners.ToList(),
                BigBanners= _context.BigBanners.ToList(),
                Products= _context.Products.ToList()
            };
            return View(homeViewModel);
        }
        public IActionResult AboutUs()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Statistics = _context.Statistics.ToList(),
                TeamMembers= _context.TeamMembers.Include(x=>x.Position).ToList()  
            };
            return View(homeViewModel);
        }
        public IActionResult Blog()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Blogs = _context.Blogs.ToList()
            };
            return View(homeViewModel);
        }
       
        public IActionResult Contact()
        {
            return View();
        }
   
       public IActionResult Wishlist()
        {
            return View();
        }
      
        public IActionResult Shop()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Products = _context.Products.ToList()
            };
            return View(homeViewModel);
        }
        public IActionResult Cart()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Products = _context.Products.ToList()
            };
            return View(homeViewModel);
        }
        public IActionResult SingleProduct()
        {
            return View();
        }
        public IActionResult Faq()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
        public IActionResult GetProduct(int id)
        {
            Product product=_context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();
            return PartialView("_ProductModalPartial",product);
        }
        public IActionResult GetSingleProduct(int id)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();
            return PartialView("_SingleProductPartial", product);
        }
        public IActionResult SetSession(int id)
        {
            HttpContext.Session.SetString("UserId", id.ToString());
            return Content("Added Session");
        }
        public IActionResult GetSession()
        {
            string userId=HttpContext.Session.GetString("UserId");
            return Content(userId);
        }
    }
}