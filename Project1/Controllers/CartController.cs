using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project1.Data;
using Project1.Models;
using Project1.Models.ViewModels;
using Project1.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Project1.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SesstionCart) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SesstionCart).Count() > 0)
            {
                // Session Exist
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SesstionCart);
            }
            List<int> prodInCart = shoppingCartList.Select(i => i.PoductId).ToList();
            IEnumerable<Product> prodList = _db.Product.Where(u => prodInCart.Contains(u.Id));
            return View(prodList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {

            return RedirectToAction(nameof(Summery));

        }
        public IActionResult Summery()
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SesstionCart) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SesstionCart).Count() > 0)
            {
                // Session Exist
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SesstionCart);
            }
            List<int> prodInCart = shoppingCartList.Select(i => i.PoductId).ToList();
            IEnumerable<Product> prodList = _db.Product.Where(u => prodInCart.Contains(u.Id));

            ProductUserVM = new ProductUserVM
            {
                ApplicationUser = _db.ApplicationUser.FirstOrDefault(u => u.Id == claim.Value),
                ProductList = prodList, 
            };

            return View(ProductUserVM);


        }

        public IActionResult Remove(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SesstionCart) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SesstionCart).Count() > 0)
            {
                // Session Exist
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SesstionCart);
            }
            shoppingCartList.Remove(shoppingCartList.FirstOrDefault(u => u.PoductId == id));
            HttpContext.Session.Set(WC.SesstionCart, shoppingCartList);


            return RedirectToAction(nameof(Index));
        }
    }
}
