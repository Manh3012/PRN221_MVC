using DAL;
using BAL.Model;
using System.Linq;
using DAL.Entities;
using Newtonsoft.Json;
using PRN221_MVC.Models;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http.Extensions;

namespace PRN221_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FRMDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger,FRMDbContext _dbContext)
        {
            this._dbContext = _dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
           var category =_dbContext.Category.ToList();


            return View(category);
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        public IActionResult LoginClient()
        {
            return View();
        }
        public IActionResult AddToWishList(string sessionValue)
        {

            var product = JsonConvert.DeserializeObject<Product>(sessionValue);

            // Get the session object from the HttpContext
            var session = HttpContext.Session;

            // Retrieve the current wishlist from session state
            var wishlistJson = session.GetString("WishList");
            var wishlist = !string.IsNullOrEmpty(wishlistJson) ? JsonConvert.DeserializeObject<List<Product>>(wishlistJson) : new List<Product>();

            // Check if the wishlist already contains the product being added
            if (!wishlist.Any(p => p.ID == product.ID))
            {
                // Add the new product to the wishlist
                wishlist.Add(product);

                // Store the updated wishlist back in session state
                session.SetString("WishList", JsonConvert.SerializeObject(wishlist));
            }
                RedirectToAction("WishList", "Order");
            // Redirect to the wishlist page
            return RedirectToAction("Category",new { id= product.Category.ID});
        }


        [HttpGet]
        public IActionResult Category(int sortId,int id)
        {
            List<Product> sort = new List<Product>();
           Category category = new Category();

           if(id == 0) {
                return RedirectToAction("Index");
            }
                switch (sortId)
                {

                    case 1:
                        sort = _dbContext.Product.Include(x => x.Category).Where(x => x.Category.ID == id).OrderBy(x => x.Name).ToList();
                        ViewBag.TotalProduct = sort.Count();
                        break;
                    case 2:
                        sort = _dbContext.Product.Include(x => x.Category).Where(x => x.Category.ID == id).OrderByDescending(x => x.Name).ToList();
                        ViewBag.TotalProduct = sort.Count();
                        break;
                    case 3:
                        sort = _dbContext.Product.Include(x => x.Category).Where(x => x.Category.ID == id).OrderBy(x => x.Price).ToList();
                        ViewBag.TotalProduct = sort.Count();
                        break;
                    case 4:
                        sort = _dbContext.Product.Include(x => x.Category).Where(x => x.Category.ID == id).OrderByDescending(x => x.Price).ToList();
                        ViewBag.TotalProduct = sort.Count();
                        break;
                    default:
                        sort = _dbContext.Product.Include(x => x.Category).Where(x => x.Category.ID == id).ToList();
                        category = _dbContext.Category.FirstOrDefault(x => x.ID == id);
                        foreach (var item in sort)
                        {
                            item.Category = category;
                        }
                        ViewBag.TotalProduct = sort.Count();
                        break;


                }
            

            return View(sort);
        }


        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Details(int id)
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