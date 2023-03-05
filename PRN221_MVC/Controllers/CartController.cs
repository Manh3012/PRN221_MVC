using BAL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;

namespace PRN221_MVC.Controllers
{
    public class CartController : Controller
    {   

        ICartService CartService;
        IUserService UserService;

        public CartController(ICartService cartService, IUserService userService)
        {
            this.CartService = cartService;
            this.UserService = userService;
        }
    
        public IActionResult Index()
        {
            User user = new User();
            user.Email = HttpContext.Session.GetString("_Email");
            if (user.Email == string.Empty || user.Email == null)
            {
                user.Email = "anhthuyn2412@gmail.com";
            }
            var carts = CartService.GetCartList(user);
            ViewData["carts"] = carts.Result;
            return View("/Views/OrderDetail/Cart.cshtml");
        }

        [HttpGet]
        public JsonResult InCart()
        {
            User user = new User();
            user.Email = HttpContext.Session.GetString("_Email");
            Console.WriteLine(HttpContext.User.ToString());
            var carts = CartService.GetCartList(user);
            return Json(carts.Result);
        }

        public ActionResult Delete(string id)
        {
            var cart = CartService.GetCartById(id);
            CartService.DeleteItem(cart);
            return View("/Views/OrderDetail/Cart.cshtml");
        }

        [HttpPost]
        public ActionResult Update(string id, int quantity)
        {
            var cart = CartService.GetCartById(id);
            CartService.UpdateQuantity(cart, quantity);
            User user = new User();
            user.Email = HttpContext.Session.GetString("_Email");
            if (user.Email == string.Empty || user.Email == null)
            {
                user.Email = "anhthuyn2412@gmail.com";
            }
            var carts = CartService.GetCartList(user);
            ViewData["carts"] = carts.Result;
            return View("/Views/OrderDetail/Cart.cshtml");
        }
    }
}
