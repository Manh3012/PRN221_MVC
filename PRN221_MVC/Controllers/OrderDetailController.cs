using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace PRN221_MVC.Controllers
{
    public class OrderDetailController : Controller
    {
        // GET: OrderDetailController
        public ActionResult Index()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }

        // GET: OrderDetailController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderDetailController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderDetailController/Create
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

        // GET: OrderDetailController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderDetailController/Edit/5
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

        // GET: OrderDetailController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderDetailController/Delete/5
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
