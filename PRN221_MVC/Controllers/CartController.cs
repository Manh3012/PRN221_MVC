using BAL.Services.Interface;
using DAL.Entities;
using DAL.Repositories.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRN221_MVC.Models;
using System.Diagnostics;
using System.Net;
using System.Security;
using System.Security.Claims;

namespace PRN221_MVC.Controllers
{
    public class CartController : Controller
    {

        ICartService CartService;
        IUserService UserService;
        IProductService ProductService;

        public CartController(ICartService cartService, IUserService userService, IProductService productService)
        {
            this.CartService = cartService;
            this.UserService = userService;
            this.ProductService = productService;
        }

        public IActionResult Index()
        {
            string? email = HttpContext.Session.GetString("_Email");
            if (email != null)
            {
                User? User = UserService.GetUserByEmail(email);
                if (User != null)
                {
                    ViewData["carts"] = CartService.GetCartList(User);
                }
            }
            return View("/Views/Cart/Cart.cshtml");
        }

        [HttpPost]
        public JsonResult Add()
        {
            if (HttpContext.Session.GetString("_Email") == null)
            {
                return Json(new { Status = "Error", Message = "Session Ended. Please login again" });
            }
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    string email = HttpContext.Session.GetString("_Email") ?? "anhthuyn2412@gmail.com";
                    var cartModel = JsonConvert.DeserializeObject<CartViewModel>(requestBody);
                    Product product = ProductService.GetProductById(cartModel.ProductID);
                    User user = UserService.GetUserByEmail(cartModel.UserEmail);
                    if (user != null)
                    {
                        Cart cart = CartService.GetCartByProductAndUser(product, user);
                        if (cart != null)
                        {
                            CartService.UpdateQuantity(cart, cart.Quantity + cartModel.Quantity);
                        }
                        else
                        {
                            cart = new Cart();
                            cart.Quantity = cartModel.Quantity;
                            cart.User = user;
                            cart.Product = product;
                            CartService.AddItem(cart);
                        }
                        return Json(new { Status = "Success", Message = product.Name + " has been added to your cart" });
                    }
                    else
                    {
                        return Json(new {Status="Error", Message="You need to login first to use this function."});
                    }
                }
            }
            return Json(new { Status = "Error", Message = "Something went wrong. Please try again" });
        }

        [HttpGet]
        public JsonResult InCart()
        {
            if (HttpContext.Session.GetString("_Email") == null)
            {
                return Json(new { Status = "Error", Message = "Session Ended. Please login again" });
            }
            User user = new User();
            user.Email = HttpContext.Session.GetString("_Email");
            Console.WriteLine(HttpContext.User.ToString());
            var carts = CartService.GetCartList(user);
            return Json(carts);
        }

        [HttpPost]
        public ActionResult Delete()
        {
            if (HttpContext.Session.GetString("_Email") == null)
            {
                return Json(new { Status = "Error", Message = "Session Ended. Please login again" });
            }
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    //TODO - to be updated
                    try
                    {
                        string email = HttpContext.Session.GetString("_Email") ?? "anhthuyn2412@gmail.com";
                        var cartModel = JsonConvert.DeserializeObject<CartViewModel>(requestBody);
                        Product product = ProductService.GetProductById(cartModel.ProductID);
                        User user = UserService.GetUserByEmail(cartModel.UserEmail);
                        if (cartModel != null)
                        {
                            Cart cart = CartService.GetCartByProductAndUser(product, user);
                            if (cart != null)
                            {
                                CartService.DeleteItem(cart);
                                return Json(new { Status = "Success", Message = "Deleted successful" });
                            }
                        }
                    }
                    catch (Exception e )
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            return Json(new { Status = "Error", Message = "Something went wrong. Please try again" });
        }

        [HttpPatch]
        public ActionResult Update() {
            if (HttpContext.Session.GetString("_Email") == null)
            {
                return Json(new { Status = "Error", Message = "Session Ended. Please login again" });
            }
            MemoryStream stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    //TODO - to be updated
                    string email = HttpContext.Session.GetString("_Email") ?? "anhthuyn2412@gmail.com";
                    var cartModel = JsonConvert.DeserializeObject<CartViewModel>(requestBody);
                    Product product = ProductService.GetProductById(cartModel.ProductID);
                    User user = UserService.GetUserByEmail(cartModel.UserEmail);
                    if (cartModel != null)
                    {
                        Cart cart = CartService.GetCartByProductAndUser(product, user);
                        if (cart != null)
                        {
                            CartService.UpdateQuantity(cart, cartModel.Quantity);
                            return Json(new { Status = "Success", Message = "Updated successful" });
                        }
                    }
                }
            }
            return Json(new { Status = "Error", Message = "Something went wrong. Please try again" });
        }
    }
}
