using BAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DAL.Repositories.Interface;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Data.OleDb;
using System.Text.Unicode;
using NuGet.Protocol.Plugins;
using PRN221_MVC.Models;

namespace PRN221_MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private UserManager<User> _userManager;
        private SignInManager<User> signInManager;

        FRMDbContext context = new FRMDbContext();
        public AdminController(IUserService userService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userService = userService;
            _userManager = userManager;
            this.signInManager = signInManager;
        }
        // GET: AdminController
        public async Task<ActionResult> Index(int id)
        {
            var user= await _userService.GetById(id.ToString());
            if (user != null)
            {
                return View(model:user);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Login()
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
        public async Task<ActionResult> AsyncLogin(LoginUserViewModel login)
        {
            if (ModelState.IsValid)
            {
                User appUser = await _userManager.FindByEmailAsync(login.Email);

                var roleStore = new RoleStore<IdentityRole>(context);
                var roles = roleStore.Roles.ToList();


                if (appUser != null)
                {
                    var role = await _userManager.GetRolesAsync(appUser);

                    var matchingRole = roles.FirstOrDefault(r => role.Contains(r.Name)).Name;

                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, login.Password, false, false);

                    if (result.Succeeded)
                    {
                        if (matchingRole != null && matchingRole.Equals("Administrator"))
                        {
                            ViewBag.User = appUser;
                            return View("AdminProfile");
                        }

                        if (matchingRole != null && matchingRole.Equals("ShopOwner"))
                        {
                            HttpContext.Session.SetString("Email", appUser.Email);
                            return RedirectToAction("IndexShopOwner", "ShopOwner");
                        }
                    }

                    //return Redirect(login.ReturnUrl ?? "/");

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
        }
        public ActionResult Recover_Password()
        {
            return View();
        }

        public async Task<ActionResult> UserDetail(int id)
        {
            var user = await _userService.GetById(id.ToString());
            if (user != null)
            {
                return View(model: user);
            }
            else
            {
                return View();
            }
        }
        public ActionResult Error()
        {
            return View();
        }
        public async Task<ActionResult> AdminProfile(int id)
        {
            var user = await _userService.GetById(id.ToString());
            if (user != null)
            {
                return View(model: user);
            }
            else
            {
                return View();
            }
        }
        public ActionResult Sales_Analytics()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> UserAdd()
        {
            try
            {
                string password = HttpContext.Request.Form["userpassword"];
                var newUser = new User
                {
                    UserName = HttpContext.Request.Form["username"],
                    Email = HttpContext.Request.Form["useremail"],
                    PhoneNumber = HttpContext.Request.Form["mobilenumber"],
                    DoB = DateTime.Parse("01/01/2000"),
                    Gender = "F",
                    Address = "NaN",
                    isDeleted = false,
                    Name = "erererere",
                };

                //string password = collection["userpassword"];
                //var newUser = new User
                //{
                //    UserName = collection["username"],
                //    Email = collection["useremail"],
                //    PhoneNumber = collection["mobile number"],
                //    DoB = DateTime.Parse("01/01/2000"),
                //    Gender = "F",
                //    Address = "NaN",
                //    isDeleted = false,
                //    Name = "erererere",
                //};
                IdentityResult re = await _userManager.CreateAsync(newUser, password);
                //IdentityResult re1 = await _userManager.AddToRoleAsync(newUser, "admin");
                //IdentityResult a = await _roleManager.CreateAsync(new IdentityUserRole<string>().RoleId = newUser.Id);
                await _userManager.AddToRoleAsync(newUser, "Administrator");
                return RedirectToAction("Index");
            }
            catch
            {
                Console.WriteLine("Error");
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
        public async Task<ActionResult> Edit(string id, IFormCollection collection)
        {
            try
            {

                return View();
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> ChangePass()
        {
            try
            {
                string userid = HttpContext.Request.Form["idhidden"];
                var user = await _userManager.FindByIdAsync(userid);
                string currentPass = HttpContext.Request.Form["oldpass"];
                string newPass = HttpContext.Request.Form["newpass"];

                string decodeHash = base64Decode(user.PasswordHash);

                string newHashPassword = _userManager.PasswordHasher.HashPassword(user, newPass);
                if (user == null)
                {
                    return BadRequest("User not found");
                }
                var result = await _userManager.ChangePasswordAsync(user, currentPass, newPass);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            //var db = new FRMDbContext();
            //var model = db.Users.Find(id.ToString());
            //db.Users.Remove(model);
            //db.SaveChanges();
            //return RedirectToAction("UserList");
            var user = await _userService.GetById(id.ToString());
            if (user != null)
            {
                var db = new FRMDbContext();
                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("UserList");
            }
            else {
                return RedirectToAction("UserList");
            }
            
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var getUserbyId = users.FirstOrDefault(u => Int32.Parse(u.Id) == id);
                if (getUserbyId != null)
                {
                    using (var db = new FRMDbContext())
                    {
                        db.Users.Remove(getUserbyId);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("UserList");
            }
            catch
            {
                return View();
            }
        }


        public string hashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPass = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPass);
        }

        public static string base64Decode(string sData) //Decode
        {
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(sData);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }

    }
}
