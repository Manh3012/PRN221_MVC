﻿using BAL.Services.Implements;
using DAL.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DAL.Entities;

namespace PRN221_MVC.Controllers
{
    public class ProductController : Controller
    {
        readonly IProductService productService = new ProductService();

        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: ProductController/Create
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

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
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

        public ActionResult SearchProduct(string search)
        {
            var items = productService.Search(search);
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

        public ActionResult Filter(int id)
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
            return View("Filter");
        }

        public ActionResult ProSort()
        {

            return View();
        }
    }
}
