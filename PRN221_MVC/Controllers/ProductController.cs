﻿using DAL;
using DAL.Entities;
using BAL.Services.Implements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

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
            var productDetail = _dbContext.Product.FirstOrDefault(x => x.ID == id);
            if (productDetail == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(productDetail);
        }
        [BindProperty]
        public Product Product { get; set; }

        [HttpGet]
        public async Task<IActionResult> ViewProduct(int id)
        {
            var productDetail = await _dbContext.Product.FirstOrDefaultAsync(x => x.ID == id);
            return View(productDetail);
        }
        [HttpPost]
        public async Task<IActionResult> ViewProduct(Product product)
        {
            var productDetail = await _dbContext.Product.FindAsync(product.ID);
            if (productDetail != null)
            {
                productDetail.Name = product.Name;
                productDetail.Price = product.Price;
                productDetail.imgPath = product.imgPath;
                productDetail.isDeleted = product.isDeleted;
                productDetail.isAvailable = product.isAvailable;
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
            var productList = await _dbContext.Product.ToListAsync();
            return View(productList);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (_dbContext.Product.Any(p => p.Name == product.Name))
            {
                ModelState.AddModelError($"Name", "A product with this name already exists.");
                return View(product);
            }
            if (ModelState.IsValid)
            {

                var products = new Product()
                {
                    Name = product.Name,
                    Price = product.Price,
                    imgPath = product.imgPath,
                    isAvailable = product.isAvailable,
                    isDeleted = product.isDeleted,
                };
                await _dbContext.Product.AddAsync(products);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("CreateProduct");
            }
            return View(product);
        }

        public ActionResult SearchProduct(string search)
        {
            var items = productService.Search(search.Trim());
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
            return View("Filter");
        }

        public ActionResult Filter(int sortId, int id)
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
            Category category = new Category();

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
        public IActionResult Sort(int sortId, int id)
        {
            List<Product> sort = new List<Product>();
            Category category = new Category();

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
    }
}
