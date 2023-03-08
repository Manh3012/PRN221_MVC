using DAL;
using BAL.Model;
using System.Data;
using DAL.Entities;
using Newtonsoft.Json;
using BAL.Services.Implements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace PRN221_MVC.Controllers
{
    public class ProductController : Controller
    {
        readonly IProductService productService = new ProductService();
        private readonly FRMDbContext _dbContext;

        public ProductController(FRMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var productDetail = _dbContext.Product
                                            .Include(v => v.Comments)
                                            .FirstOrDefault(x => x.ID == id);
            if (productDetail == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(productDetail);
        }

        public ActionResult DeleteComment()
        {
            return RedirectToAction("Details");
        }

        [BindProperty]
        public Product Product { get; set; }

        [HttpGet]
        public async Task<IActionResult> ViewProduct(int id)
        {
            ViewBag.CategorySelective = _dbContext.Category.ToList();

            var productDetail = await _dbContext.Product.Include(x=>x.Category).FirstOrDefaultAsync(x => x.ID == id);
            return View(productDetail);
        }

        
        [HttpPost]
        public async Task<IActionResult> ViewProduct(Product product)
        {
            
            ViewBag.CategorySelective = _dbContext.Category.ToList();

            var productDetail = await _dbContext.Product.FindAsync(product.ID);
            if (productDetail != null)
            {
                productDetail.Name = product.Name;
                productDetail.Price = product.Price;
                productDetail.imgPath = product.imgPath;
                productDetail.isDeleted = product.isDeleted;
                productDetail.isAvailable = product.isAvailable;
                var category = await _dbContext.Category.FindAsync(product.Category.ID);
                if (category != null)
                {
                    productDetail.Category = category;
                }
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("ListProduct");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Product product)
        {
            var productdetail = await _dbContext.Product.FindAsync(product.ID);
            if (productdetail != null)
            {
                productdetail.isDeleted = true;
                _dbContext.Product.Update(productdetail);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("ListProduct");
            }
            else
            { return RedirectToAction("ListProduct"); }
        }

        [HttpGet]
        public async Task<IActionResult> ListProduct()
        {
            
            var productList = await _dbContext.Product.Include(x=>x.Category).ToListAsync();
            return View(productList);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            ViewBag.CategorySelect = _dbContext.Category.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductModel product)
        {
            ViewBag.CategorySelect = _dbContext.Category.ToList();

            if (_dbContext.Product.Any(p => p.Name == product.Name))
            {
                ModelState.AddModelError($"Name", "A product with this name already exists.");
                return View(product);
            }
            if (ModelState.IsValid)
            {

                Category category=_dbContext.Category.FirstOrDefault(x=>x.ID== product.CategoryID);
                var products = new Product()
                {
                    Name = product.Name,
                    Price = product.Price,
                    imgPath = product.imgPath,
                    isAvailable = product.isAvailable,
                    isDeleted = product.isDeleted,
                    Category = category,
                    
                };
                await _dbContext.Product.AddAsync(products);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("CreateProduct");
            }
            return View(product);
        }

        public ActionResult SearchProduct(string? search)
        {
            List<Product> items = new List<Product>();

            try
            {

                items = productService.Search(search.Trim());
                if (items.Count == 0)
                {
                    //ViewBag.Message = "Nothing Found";
                    //TempData["Message"] = ViewBag.Message;
                    //return View("/Views/Home/Index.cshtml");
                    ViewBag.Error = "No Item Found";
                }
                else
                {
                    ViewBag.Count = items.Count;
                    ViewBag.Search = items;
                }
            }
            catch
            {
                items=_dbContext.Product.ToList();
                ViewBag.Search = items;
                ViewBag.Count = items.Count;

                return View("Filter",items);
            }
            return View("Filter");
        }

        public ActionResult Filter(int sortId, int id,List<Product> list)
        {
            var products = productService.Filter(id);
            if (products.Count > 0)
            {
                ViewBag.Count = products.Count;
                ViewBag.Show = products;
            }
            else
            {
                ViewBag.Error = "No Item Found";
            }
            List<Product> sort = new List<Product>();
            List<Product> listProduct = _dbContext.Product.ToList();
            Category category = new Category();
            var cate = _dbContext.Category.FirstOrDefault(x => x.ID == id);

            if (id == 0)
            {
                return NotFound();
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
        [HttpGet]
        public IActionResult Sort(int sortId)
        {

            List<Product> sort = new List<Product>();
            //switch (sortId)
            //{

            //    case 1:
            //           sort =list.OrderBy(x=>x.Name).ToList();
            //        break;
            //    case 2:
            //           sort =list.OrderByDescending(x=>x.Name).ToList();

            //        break;
            //    case 3:
            //           sort =list.OrderBy(x=>x.Price).ToList();

            //        break;
            //    case 4:
            //           sort =list.OrderByDescending(x=>x.Price).ToList();

            //        break;
            //    default:
            //        sort = list.ToList();
            //        break;


            //}
            switch (sortId)
            {

                case 1:
                    sort = _dbContext.Product.OrderBy(x => x.Name).ToList();
                    ViewBag.TotalProduct = sort.Count();
                    break;
                case 2:
                    sort = _dbContext.Product.OrderByDescending(x => x.Name).ToList();
                    ViewBag.TotalProduct = sort.Count();
                    break;
                case 3:
                    sort = _dbContext.Product.OrderBy(x => x.Price).ToList();
                    ViewBag.TotalProduct = sort.Count();
                    break;
                case 4:
                    sort = _dbContext.Product.OrderByDescending(x => x.Price).ToList();
                    ViewBag.TotalProduct = sort.Count();
                    break;
                default:
                    sort = _dbContext.Product.ToList();
                    ViewBag.TotalProduct = sort.Count();
                    break;


            }
            ViewBag.Search = sort;

            return View("Filter",sort);
        }

        public ActionResult ProSort()
        {

            return View();
        }
    }
}
