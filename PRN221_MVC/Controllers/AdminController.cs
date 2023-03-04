using BAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DAL.Repositories.Interface;
using DAL.Entities;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Data.OleDb;
using System.Text.Unicode;
using NuGet.Protocol.Plugins;
using PRN221_MVC.Models;
>>>>>>> parent of 1f234ae (Check role in AsyncLogin in Admin Controller)

namespace PRN221_MVC.Controllers
{
    public class AdminController : Controller
    {
<<<<<<< HEAD
        private readonly IOrdersService ordersService;

        public float TotalSalesToday { get; set; }

        public AdminController(IOrdersService ordersService)
=======
        private readonly IUserService _userService;
        private UserManager<User> _userManager;
        private SignInManager<User> signInManager;
        public AdminController(IUserService userService, UserManager<User> userManager, SignInManager<User> signInManager)
>>>>>>> parent of 1f234ae (Check role in AsyncLogin in Admin Controller)
        {
            this.ordersService = ordersService;
        }


        // GET: AdminController
        public ActionResult Index()
        {
            var totalToday = ordersService.GetTotalOrderToday();
            var totalWeek = ordersService.GetTotalOrdersWeek();
            var total30Days = ordersService.GetTotalOrderLastThirtyDays();
            var countOrders30Days = ordersService.CountOrderLastThirtyDays();
            var monthLySalesData = ordersService.GetMonthlySalesData(2022);
            var topSellingProductByMonth = ordersService.GetTopSellingProductsByMonth();
            var topSellingProductByWeek = ordersService.GetTopSellingProductsByWeek();
            var orderValuesInEachMonth = ordersService.GetOrderValuesInEachMonth();
            var salesDataInEachMonth = ordersService.GetSalesDataMonthly(2023);

            foreach (var item in orderValuesInEachMonth)
            {
                Console.WriteLine("Month | Total Amount | Total Quantity");

                Console.WriteLine(item.Key);
                Console.WriteLine(item.Value);
            }


            ViewBag.TotalSalesToday = totalToday;

            ViewBag.TotalSalesWeek = totalWeek;

            ViewBag.Total30Days = total30Days;

            ViewBag.CountOrders30Days = countOrders30Days;

            ViewBag.TopSellingProductByMonth = topSellingProductByMonth;

            ViewBag.TopSellingProductByWeek = topSellingProductByWeek;

            ViewBag.MonthLySalesData = monthLySalesData;

            ViewBag.OrderValuesInEachMonth = orderValuesInEachMonth;

            ViewBag.SalesDataInEachMonth = salesDataInEachMonth;
            return View();
        }

        public ActionResult OrderHistory()
        {

            var orders = ordersService.GetOrders();


            ViewBag.Orders = orders;

            return View();
        }

        public ActionResult OrderDetails(Guid id)
        {
            var orderDetails = ordersService.GetOrderDetailsByOrderId(id);
            var order = ordersService.GetOrderById(id);
            float subtotals = 0;
            foreach(var item in orderDetails)
            {
                subtotals += + item.Product.Price * item.Quantity;
            }

            ViewBag.Order = order;
            ViewBag.OrderDetails = orderDetails;
            ViewBag.SubTotals = subtotals;

            return View();
        }

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
            return View(model:users);
        }
        public ActionResult Users()
        {
            return View();
        }
        public ActionResult Login()
        {
<<<<<<< HEAD
            return View();
=======
            if (ModelState.IsValid)
            {
                User appUser = await _userManager.FindByEmailAsync(login.Email);
                if (appUser != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, login.Password, false, false);

                    if (result.Succeeded)
                        //return Redirect(login.ReturnUrl ?? "/");
                        return RedirectToAction("Index", "Home");

                    // Two Factor Authentication
                    if (result.RequiresTwoFactor)
                    {
                        //return RedirectToAction("LoginTwoStep", new { appUser.Email, login.ReturnUrl });
                        return RedirectToAction("LoginTwoStep", new { appUser.Email });
                    }

                    // Email confirmation 
                    bool emailStatus = await _userManager.IsEmailConfirmedAsync(appUser);
                    if (emailStatus == false)
                    {
                        ModelState.AddModelError(nameof(login.Email), "Email is unconfirmed, please confirm it first");
                    }

                    // https://www.yogihosting.com/aspnet-core-identity-user-lockout/
                    /*if (result.IsLockedOut)
                        ModelState.AddModelError("", "Your account is locked out. Kindly wait for 10 minutes and try again");*/
                }
                ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password");
            }
            return RedirectToAction("Login");
>>>>>>> parent of 1f234ae (Check role in AsyncLogin in Admin Controller)
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
                return RedirectToAction(nameof(Index));
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
