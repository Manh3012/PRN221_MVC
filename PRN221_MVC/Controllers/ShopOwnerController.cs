using DAL.Entities;
using DAL.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PRN221_MVC.Controllers {
    public class ShopOwnerController : Controller {
        private UserManager<User> _userManager;
        private readonly IOrdersService ordersService;
        public ShopOwnerController(IOrdersService ordersService, UserManager<User> userManager) {
            this.ordersService = ordersService;
            _userManager = userManager;
        }

        // GET: ShopOwnerController
        public ActionResult IndexShopOwner() {
            if (HttpContext.Session.GetString("Email") == null) {
                return RedirectToAction("Login", "Admin");
            }
            var totalToday = ordersService.GetTotalOrderToday();
            var totalWeek = ordersService.GetTotalOrdersWeek();
            var total30Days = ordersService.GetTotalOrderLastThirtyDays();
            var countOrders30Days = ordersService.CountOrderLastThirtyDays();
            var monthLySalesData = ordersService.GetMonthlySalesData(2022);
            var topSellingProductByMonth = ordersService.GetTopSellingProductsByMonth();
            var topSellingProductByWeek = ordersService.GetTopSellingProductsByWeek();
            var orderValuesInEachMonth = ordersService.GetOrderValuesInEachMonth();
            var salesDataInEachMonth = ordersService.GetSalesDataMonthly(2023);

            foreach (var item in orderValuesInEachMonth) {
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

        public ActionResult LogOutShopOwner() {
            HttpContext.Session.Remove("Email");
            return RedirectToAction("Login", "Admin");
        }

        public async Task<ActionResult> ShopOwnerProfile() {
            string? email = HttpContext.Session.GetString("Email");
            User appUser = await _userManager.FindByEmailAsync(email);
            Console.WriteLine(appUser.Email);
            ViewBag.User = appUser;
            return View();
        }

        public ActionResult OrderHistory() {

            var orders = ordersService.GetOrders();


            ViewBag.Orders = orders;

            return View();
        }

        public ActionResult OrderDetails(Guid id) {
            var orderDetails = ordersService.GetOrderDetailsByOrderId(id);
            var order = ordersService.GetOrderById(id);
            float subtotals = 0;
            foreach (var item in orderDetails) {
                subtotals += +item.Product.Price * item.Quantity;
            }

            ViewBag.Order = order;
            ViewBag.OrderDetails = orderDetails;
            ViewBag.SubTotals = subtotals;

            return View();
        }

        // GET: ShopOwnerController/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // GET: ShopOwnerController/Create
        public ActionResult Create() {
            return View();
        }

        // POST: ShopOwnerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: ShopOwnerController/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: ShopOwnerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }

        // GET: ShopOwnerController/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: ShopOwnerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection) {
            try {
                return RedirectToAction(nameof(Index));
            }
            catch {
                return View();
            }
        }
    }
}
