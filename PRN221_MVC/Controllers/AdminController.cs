using BAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DAL.Repositories.Interface;
using DAL;
using DAL.Entities;

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
            return RedirectToAction("Create");
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
                if (ModelState.IsValid)
                {
                    var newUser = new User
                    {
                        Name = collection["Name"],
                        DoB = DateTime.TryParse(collection["Dob"], out var dob) ? dob : (DateTime?)null ?? DateTime.MinValue,
                        Address = collection["Address"],
                        Gender = collection["Gender"],
                        Phone = collection["Phone"]
                    };

                    using (var db = new FRMDbContext())
                    {
                        db.Users.Add(newUser);
                        db.SaveChanges();
                    }
                }
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
                var getUserbyId = users.FirstOrDefault(u => Int32.Parse(u.Id) == id);
                if(getUserbyId != null)
                {
                    getUserbyId.Name = collection["Name"];
                    getUserbyId.Phone = collection["Phone"];
                    getUserbyId.Address = collection["Address"];
                    getUserbyId.Gender = collection["Gender"];
                    getUserbyId.DoB = DateTime.TryParse(collection["Dob"], out var dob) ? dob : (DateTime?)null ?? DateTime.MinValue;
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
