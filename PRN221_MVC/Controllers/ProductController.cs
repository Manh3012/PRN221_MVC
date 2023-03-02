using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace PRN221_MVC.Controllers
{
    public class ProductController : Controller
    {
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

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var productDetail = _dbContext.Product.FirstOrDefault(x => x.ID == id);
            if(productDetail==null)
            {
                return RedirectToAction("Index","Home");
            }
            return View(productDetail);
        }
        [BindProperty]
        public Product Product { get; set; }

        [HttpGet]
        public async Task<IActionResult> ViewProduct(int id)
        {
            var productDetail= await _dbContext.Product.FirstOrDefaultAsync(x => x.ID == id);
            return View(productDetail);
        }
        [HttpPost]
        public async Task<IActionResult> ViewProduct(Product product)
        {
            var productDetail=await _dbContext.Product.FindAsync(product.ID);
            if(productDetail!=null)
            {
                productDetail.Name= product.Name;
                productDetail.Price= product.Price;
                productDetail.imgPath = product.imgPath;
                productDetail.isDeleted = product.isDeleted;
                productDetail.isAvailable= product.isAvailable;
                await _dbContext.SaveChangesAsync();
            }
            return RedirectToAction("ListProduct");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Product product)
        {
            var productdetail = await _dbContext.Product.FindAsync(product.ID);
            if(productdetail!=null)
            {
                productdetail.isDeleted = true;
                 _dbContext.Product.Update(productdetail);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("ListProduct");
            }else
            { return RedirectToAction("ListProduct"); }
        }

        [HttpGet]
        public async Task<IActionResult> ListProduct()
        {
            var productList= await _dbContext.Product.ToListAsync();
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
  


       
       


      
    }
}
