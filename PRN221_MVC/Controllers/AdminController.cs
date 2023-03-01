﻿using BAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DAL.Repositories.Interface;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace PRN221_MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: AdminController
        public ActionResult Index()
        {

            return View();
        }

        public List<User> users { get; set; }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }
        public async Task<ActionResult> UserList()
        {
            List<User> users = await _userService.GetAll();
            return View(model: users);
        }
        public ActionResult Users()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Recover_Password()
        {
            return View();
        }
        public ActionResult UserDetail()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult AdminProfile()
        {
            return View();
        }
        public ActionResult Sales_Analytics()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var newUser = new User
                {
                    Name = HttpContext.Request.Form["username"],
                    Email = HttpContext.Request.Form["useremail"],
                    PasswordHash = HttpContext.Request.Form["userpassword"].GetHashCode().ToString(),
                    PhoneNumber = HttpContext.Request.Form["mobile number"],
                    DoB = DateTime.Parse("01/01/2000"),
                    Gender = "F",
                    Address = "NaN",
                    isDeleted = false,
                    UserName = "",
                    PhoneNumberConfirmed = false,
                    AccessFailedCount = 0,
                    ConcurrencyStamp = "",
                    EmailConfirmed = false,
                };
                using (var db = new FRMDbContext())
                {
                    db.Users.Add(newUser);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }


        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var getUserbyId = users.FirstOrDefault(u => Int32.Parse(u.Id) == id);
                if (getUserbyId != null)
                {
                    getUserbyId.PasswordHash = collection["password"].GetHashCode().ToString();
                }

                using (var db = new FRMDbContext())
                {
                    db.Users.Update(getUserbyId);
                    db.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
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
