using BAL.Helpers;
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


        public UserController(UserManager<User> userMgr, SignInManager<User> signinMgr, IPasswordHasher<User> passwordHasher) {
            userManager = userMgr;
            signInManager = signinMgr;
            this.passwordHasher = passwordHasher;
        }

        public async Task<IActionResult> Logout() {
            await signInManager.SignOutAsync();
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
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);

                    EmailHelper emailHelper = new EmailHelper();
                    bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);

                    if (emailResponse)
                        return RedirectToAction("Index", "Home");
                    else {
                        // log email failed 
                        return View("/Views/Client/Email/Unconfirm.cshtml");
                    }
                }
                else {
                    // Input fields are not valid
                    //string error = string.Join(", ", result.Errors);
                    //ModelState.AddModelError("error", error);
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

        public IActionResult Login() {
            return View("/Views/Home/LoginClient.cshtml");
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
                return View("/Views/Home/LoginClient.cshtml");
            }
        }
    }
}
