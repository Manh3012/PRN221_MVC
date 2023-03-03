using DAL.Entities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace PRN221_MVC.Controllers
{
    public class OrderController : Controller
    {
        // GET: OrderController
        public ActionResult Index()
        {
            return View();
        }
        public IActionResult WishList()
        {
            var session = HttpContext.Session;

            // Retrieve the wishlist from session state
            var wishlistJson = session.GetString("WishList");
            var listProduct = !string.IsNullOrEmpty(wishlistJson) ? JsonConvert.DeserializeObject<List<Product>>(wishlistJson) : new List<Product>();

            // Pass the wishlist to the view
            return View(listProduct);
        }
        [HttpPost]
        public IActionResult RemoveFromWishList(int id)
        {
            // Retrieve the current wishlist from session state
            var wishlistJson = HttpContext.Session.GetString("WishList");
            var wishlist = !string.IsNullOrEmpty(wishlistJson) ? JsonConvert.DeserializeObject<List<Product>>(wishlistJson) : new List<Product>();

            // Remove the product with the specified ID from the wishlist
            var productToRemove = wishlist.FirstOrDefault(p => p.ID == id);
            if (productToRemove != null)
            {
                wishlist.Remove(productToRemove);

                // Store the updated wishlist back in session state
                HttpContext.Session.SetString("WishList", JsonConvert.SerializeObject(wishlist));
            }

            // Redirect back to the wishlist page
            return RedirectToAction("WishList");
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
