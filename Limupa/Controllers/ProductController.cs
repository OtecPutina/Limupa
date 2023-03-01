using Limupa.Context;
using Limupa.Models;
using Limupa.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace Limupa.Controllers
{
    public class ProductController : Controller
    {
        public ProductController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public AppDbContext _context { get; }
        public UserManager<AppUser> _userManager { get; }

        public IActionResult Index()
        {
            return View();  
        }

        public async Task<IActionResult> AddToBasket(int productId)
        {
            if (!_context.Products.Any(x => x.Id == productId)) return NotFound();
            List<BasketItemViewModel> basketItems= new List<BasketItemViewModel>();
            BasketItemViewModel basketItem = null;
            AppUser member = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
            member = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
            string basketItemsStr = HttpContext.Request.Cookies["BasketItems"];
            if(member == null)
            {
                if (basketItemsStr != null)
                {
                    basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);

                    basketItem = basketItems.FirstOrDefault(x => x.ProductId == productId);
                    if (basketItem != null) basketItem.Count++;
                    else
                    {
                        basketItem = new BasketItemViewModel
                        {
                            ProductId = productId,
                            Count = 1
                        };
                        basketItems.Add(basketItem);
                    }
                }
                else
                {
                    basketItem = new BasketItemViewModel
                    {
                        ProductId = productId,
                        Count = 1
                    };
                    basketItems.Add(basketItem);
                }
                basketItemsStr = JsonConvert.SerializeObject(basketItems);
                HttpContext.Response.Cookies.Append("BasketItems", basketItemsStr);
            }
            else
            {
                BasketItem memberBasketItem = _context.BasketItems.FirstOrDefault(x => x.AppUserId == member.Id&&x.ProductId==productId);
                if(memberBasketItem != null)
                {
                    memberBasketItem.Count++; 
                }
                else
                {
                    memberBasketItem = new BasketItem
                    {
                        AppUserId = member.Id,
                        ProductId = productId,
                        Count = 1
                    };
                    _context.BasketItems.Add(memberBasketItem);
                }
                await _context.SaveChangesAsync();
            }
            return Ok();//200
        }
        public  IActionResult GetBasketItems()
        {
            List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();    
            string basketItemsStr = HttpContext.Request.Cookies["BasketItems"];
            if(basketItemsStr!= null)
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);
            }
            return Json(basketItems);
        }
        public async Task<IActionResult> Checkout()
        {
            AppUser member = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                member = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
            List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>(); 
            List<CheckoutItemViewModel> checkoutItems = new List<CheckoutItemViewModel>();
            CheckoutItemViewModel checkoutItem = null;
            List<BasketItem> memberBasketItems = null;
            OrderViewModel orderViewModel = null;
            string basketItemsStr = HttpContext.Request.Cookies["BasketItems"];
            if (member==null)
            {
                if (basketItemsStr != null)
                {
                    basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);
                    foreach (var item in basketItems)
                    {

                        checkoutItem = new CheckoutItemViewModel
                        {
                            Product = _context.Products.FirstOrDefault(x => x.Id == item.ProductId),
                            Count = item.Count
                        };
                        checkoutItems.Add(checkoutItem);
                    }
                }
            }
            else
            {
                memberBasketItems = _context.BasketItems.Include(x => x.Product).Where(x => x.AppUserId == member.Id).ToList();
                foreach (var item in memberBasketItems)
                {
                    checkoutItem = new CheckoutItemViewModel
                    {
                        Product = item.Product,
                        Count = item.Count
                    };
                    checkoutItems.Add(checkoutItem);

                }
            }
            orderViewModel = new OrderViewModel
            {
                CheckoutItemViewModels = checkoutItems,
                Fullname=member?.Fullname,
                Email=member?.Email
            };
            return View(orderViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async  Task<IActionResult> Order(OrderViewModel orderViewModel)
        {
            List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();
            List<CheckoutItemViewModel> checkoutItems = new List<CheckoutItemViewModel>();
            CheckoutItemViewModel checkoutItem = null;
            List<BasketItem> memberBasketItems = null;
            OrderItem orderItem = null;
            int totalPrice = 0;
            string basketItemsStr = HttpContext.Request.Cookies["BasketItems"];
            AppUser member  = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                member = await  _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
            Order order = null;
            order = new Order
            {
                Fullname=orderViewModel.Fullname,
                Country= orderViewModel.Country,
                Adress= orderViewModel.Adress,
                City= orderViewModel.City,
                Email= orderViewModel.Email,
                Note= orderViewModel.Note,
                Phone= orderViewModel.Phone,
                ZipCode= orderViewModel.ZipCode,
                OrderStatus=Enums.OrderStatus.Pending,
                AppUserId=(member!=null ? member.Id:null)

            };
            if (member == null)
            {
                if (basketItemsStr != null)
                {
                    basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);
                    foreach (var item in basketItems)
                    {
                        Product product = _context.Products.FirstOrDefault(x => x.Id == item.ProductId);
                        orderItem = new OrderItem
                        {
                            Product=product,
                            ProductName=product.Name,
                            DiscountPercent=(int)product.Discount,
                            SalePrice=(int)(product.Price*(1-(product.Discount/100))),
                            Count=item.Count,
                            Order=order
                        };
                        totalPrice += orderItem.SalePrice*orderItem.Count;
                        order.OrderItems.Add(orderItem);


                    }
                }
            }
            else
            {
                memberBasketItems = _context.BasketItems.Include(x => x.Product).Where(x => x.AppUserId == member.Id).ToList();
                foreach (var item in memberBasketItems)
                {
                    Product product = _context.Products.FirstOrDefault(x => x.Id == item.ProductId);
                    orderItem = new OrderItem
                    {
                        Product = product,
                        ProductName = product.Name,
                        DiscountPercent = (int)product.Discount,
                        SalePrice = (int)(product.Price * (1 - (product.Discount / 100))),
                        Count = item.Count,
                        Order = order
                    };
                    totalPrice += orderItem.SalePrice * orderItem.Count;
                    order.OrderItems.Add(orderItem);

                }
            }
            order.TotalPrice = totalPrice;
            _context.Orders.Add(order);
            _context.SaveChanges();


            return RedirectToAction("index", "home");
        }
  
  

    }
}
