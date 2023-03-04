using BAL.Helpers;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PRN221_MVC.Models;
using System.Security.Claims;

namespace PRN221_MVC.Controllers {
    public class UserController : Controller {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private IPasswordHasher<User> passwordHasher;
        //
        private FRMDbContext context = new FRMDbContext();

        public UserController(UserManager<User> userMgr, SignInManager<User> signinMgr, IPasswordHasher<User> passwordHasher) {
            userManager = userMgr;
            signInManager = signinMgr;
            this.passwordHasher = passwordHasher;
        }

        public async Task<IActionResult> Logout() {
            await signInManager.SignOutAsync();
            // Remove session cookie of user login info
            HttpContext.Session.Remove("UserInfo.Session");
            Response.Cookies.Delete("UserInfo.Session");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register() => View("/Views/Client/User/Register.cshtml");

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel user) {
            if (ModelState.IsValid) {
                User appUser = new User {
                    Name = user.Name,
                    UserName = user.Username,
                    Email = user.Email,
                    TwoFactorEnabled = true
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);

                if (result.Succeeded) {
                    // Add role Customer to new User
                    var userFind = await userManager.FindByNameAsync(user.Username);
                    await userManager.AddToRoleAsync(userFind, "Customer");
                    // save to db
                    context.SaveChanges();

                    var token = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);

                    EmailHelper emailHelper = new EmailHelper();
                    bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);

                    if (emailResponse)
                        return RedirectToAction("Index", "Home");
                    else {
                        // log email failed 
                        return RedirectToAction("Unconfirm", "Email");
                    }
                }
                else {
                    // Input fields are not valid
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                    //return RedirectToAction("Register");
                    ViewData["error"] = result.Errors;
                    return View("/Views/Client/User/Register.cshtml", user);
                }
            }
            else {
                // Null fields
                // send back inputed value
                return RedirectToAction("Register");
            }
        }

        [AllowAnonymous]
        public IActionResult GoogleLogin() {
            string redirectUrl = Url.Action("GoogleResponse", "User");
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }
        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse() {
            ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login));
            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            string name = info.Principal.FindFirst(ClaimTypes.Name)!.Value;
            string email = info.Principal.FindFirst(ClaimTypes.Email)!.Value;
            string[] userInfo = { name, email };
            // Set user info to session
            HttpContext.Session.SetString("_Name", name);
            HttpContext.Session.SetString("_Email", email);
            // login with Google if user is not existed -> create
            // redirect to userInfo View (return user info JSON)
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            //return View(userInfo);
            else {
                User user = new User {
                    Email = email,
                    Name = name,
                    UserName = email
                };

                IdentityResult identResult = await userManager.CreateAsync(user);
                if (identResult.Succeeded) {
                    identResult = await userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded) {
                        await signInManager.SignInAsync(user, false);
                        //return View(userInfo);
                        return RedirectToAction("Index", "Home");
                    }
                }
                // Access denied
                return View("/Views/Client/User/LoginClient.cshtml");
            }
        }

        [AllowAnonymous]
        public IActionResult Login() {
            return View("/Views/Client/User/LoginClient.cshtml");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel login) {
            if (ModelState.IsValid) {
                User appUser = await userManager.FindByEmailAsync(login.Email);
                if (appUser != null) {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, login.Password, false, false);

                    if (result.Succeeded)
                        //return Redirect(login.ReturnUrl ?? "/");
                        return RedirectToAction("Index", "Home");

                    // Two Factor Authentication
                    if (result.RequiresTwoFactor) {
                        //return RedirectToAction("LoginTwoStep", new { appUser.Email, login.ReturnUrl });
                        return RedirectToAction("LoginTwoStep", new { appUser.Email });
                    }

                    // Email confirmation 
                    bool emailStatus = await userManager.IsEmailConfirmedAsync(appUser);
                    if (emailStatus == false) {
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
        [AllowAnonymous]
        //public async Task<IActionResult> LoginTwoStep(string email, string returnUrl) {
        public async Task<IActionResult> LoginTwoStep(string email) {
            var user = await userManager.FindByEmailAsync(email);
            // set user login info to session cookie
            HttpContext.Session.SetString("_Name", user.Name);
            HttpContext.Session.SetString("_Email", email);

            var token = await userManager.GenerateTwoFactorTokenAsync(user, "Email");

            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = emailHelper.SendEmailTwoFactorCode(user.Email, token);

            return View("/Views/Client/User/LoginTwoStep.cshtml");
        }

        [HttpPost]
        [AllowAnonymous]
        //public async Task<IActionResult> LoginTwoStep(TwoFactor twoFactor, string returnUrl) {
        public async Task<IActionResult> LoginTwoStep(TwoFactor twoFactor) {
            if (!ModelState.IsValid) {
                return View(twoFactor.TwoFactorCode);
            }
            var result = await signInManager.TwoFactorSignInAsync("Email", twoFactor.TwoFactorCode, false, false);
            // if code no valid return error
            // redirect to userInfo View (return user info JSON)
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            //return Redirect(returnUrl ?? "/");
            else {
                // Remove session cookie of user login info
                HttpContext.Session.Remove("UserInfo.Session");
                Response.Cookies.Delete("UserInfo.Session");
                ModelState.AddModelError("", "Invalid Login Attempt");
                return View();
            }
        }
    }
}
