using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DAL.Repositories.Interface;
using DAL.Entities;

namespace PRN221_MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IOrdersService ordersService;

        public float TotalSalesToday { get; set; }

        public AdminController(IOrdersService ordersService)
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

            foreach(var order in orders)
            {
                Console.WriteLine($"Username: {order.User.Name}");
            }


            ViewBag.Orders = orders;

            return View();
        }

        public ActionResult OrderDetails(Guid order)
        {
            var orderDetails = ordersService.GetOrderDetailsByOrderId(order);
            
            foreach(var item in orderDetails)
            {
                Console.WriteLine("details: ", item.Amount);
            }

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
        public ActionResult UserList()
        {
            return View();
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
