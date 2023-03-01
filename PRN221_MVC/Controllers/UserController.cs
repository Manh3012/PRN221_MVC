using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PRN221_MVC.Controllers {
    public class UserController : Controller {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public UserController(UserManager<User> userMgr, SignInManager<User> signinMgr) {
            userManager = userMgr;
            signInManager = signinMgr;
        }
        public IActionResult Index() {
            return View();
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
