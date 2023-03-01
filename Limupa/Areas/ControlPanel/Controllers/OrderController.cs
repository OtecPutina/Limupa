using Limupa.Context;
using Limupa.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Limupa.Areas.ControlPanel.Controllers
{
    [Area("ControlPanel")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class OrderController : Controller
    {
        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        public AppDbContext _context { get; }

        public IActionResult Index()
        {
            List<Order> orders = _context.Orders.ToList();
            return View(orders);
        }
        public IActionResult Details(int id)
        {
            Order order = _context.Orders.Include(x=>x.OrderItems).FirstOrDefault(x=>x.Id==id);
            if (order == null) return NotFound();

            return View(order);
        }
        public IActionResult Accept(int id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null) return NotFound();
            order.OrderStatus = Enums.OrderStatus.Accepted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Reject(int id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null) return NotFound();
            order.OrderStatus = Enums.OrderStatus.Rejected;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
