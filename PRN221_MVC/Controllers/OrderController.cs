﻿using DAL.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BAL.Services.Implements;
using DAL;
using Microsoft.AspNetCore.Identity;
using DAL.Entities;

namespace PRN221_MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdersService _ordersService;
        private UserManager<User> _userManager;

        FRMDbContext context = new FRMDbContext();

        public OrderController(IOrdersService ordersService, UserManager<User> userManager)
        {
            _ordersService = ordersService;
            _userManager = userManager;
        }
        // GET: OrderController
        public ActionResult Index()
        {
            return View();
        }
        public IActionResult WishList()
        {
            return View();
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

        public async Task<ActionResult> OrderList(Guid id)
        {
            var getList = _ordersService.GetOrdersById(id);
            if (getList != null)
            {
                return View(model: getList);
            }
            else
            {
                return View();
            }
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
