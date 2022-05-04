using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project1.Data;
using Project1.Models;
using Project1.Models.ViewModels;
using Project1.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVm = new HomeVM()
            {
                products = _db.Product.Include(u => u.Catogery).Include(u => u.applicationType),
                catogeries = _db.Catogery

            };
            return View(homeVm);
        }

        public IActionResult Details(int id)
        {
            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SesstionCart) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SesstionCart).Count() > 0)
            {
                shoppingCarts = HttpContext.Session.Get<List<ShoppingCart>>(WC.SesstionCart);

            }


            var DetailsVM = new DetailsVM()
            {
                product = _db.Product.Include(u => u.Catogery).Include(u => u.applicationType).Where(u => u.Id == id).FirstOrDefault(),
                ExistInCart = false
            };
            foreach (var item in shoppingCarts)
            {
                if (item.PoductId == id)
                {
                    DetailsVM.ExistInCart = true;
                }
            }
            return View(DetailsVM);
        }

        [HttpPost, ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SesstionCart) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SesstionCart).Count() > 0)
            {
                shoppingCarts = HttpContext.Session.Get<List<ShoppingCart>>(WC.SesstionCart);

            }
            shoppingCarts.Add(new ShoppingCart
            {
                PoductId = id
            });
            HttpContext.Session.Set(WC.SesstionCart, shoppingCarts);
            return RedirectToAction(nameof(Index));
        }



        //RemoveFromCart
        public IActionResult RemoveFromCart(int id)
        {
            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SesstionCart) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SesstionCart).Count() > 0)
            {
                shoppingCarts = HttpContext.Session.Get<List<ShoppingCart>>(WC.SesstionCart);

            }


            var itemToRemove = shoppingCarts.FirstOrDefault(u => u.PoductId == id);
            if (itemToRemove!=null) {
                shoppingCarts.Remove(itemToRemove);
            }
            HttpContext.Session.Set(WC.SesstionCart, shoppingCarts);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
