using DAL;
using BAL.Model;
using DAL.Entities;
using PRN221_MVC.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

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