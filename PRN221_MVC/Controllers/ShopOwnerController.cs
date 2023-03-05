using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BAL;
using DAL.Entities;
using DAL.Repositories.Interface;

namespace PRN221_MVC.Controllers
{
    public class ShopOwnerController : Controller
    {

        private readonly IOrdersService ordersService;

        public ShopOwnerController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        // GET: ShopOwnerController
        public ActionResult IndexShopOwner()
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
            foreach (var item in orderDetails)
            {
                subtotals += +item.Product.Price * item.Quantity;
            }

            ViewBag.Order = order;
            ViewBag.OrderDetails = orderDetails;
            ViewBag.SubTotals = subtotals;

            return View();
        }

        // GET: ShopOwnerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShopOwnerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShopOwnerController/Create
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

        // GET: ShopOwnerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShopOwnerController/Edit/5
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

        // GET: ShopOwnerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShopOwnerController/Delete/5
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
