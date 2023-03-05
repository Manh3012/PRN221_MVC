using BAL.Helpers;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRN221_MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PRN221_MVC.Controllers {
    public class UserController : Controller {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        //
        private FRMDbContext context = new FRMDbContext();

        public UserController(UserManager<User> userMgr, SignInManager<User> signinMgr) {
            userManager = userMgr;
            signInManager = signinMgr;
        }

        public async Task<IActionResult> Logout() {
            await signInManager.SignOutAsync();
            // Remove session cookie of user login info
            HttpContext.Session.Remove("UserInfo.Session");
            Response.Cookies.Delete("UserInfo.Session");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register() {
            var err = TempData["RegisterError"] as string;
            if (err != null) {
                List<IdentityError> errors = System.Text.Json.JsonSerializer.Deserialize<List<IdentityError>>(err);
                ViewData["error"] = errors;
            }
            return View("/Views/Client/User/Register.cshtml");
        }

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
                    TempData["RegisterError"] = System.Text.Json.JsonSerializer.Serialize(result.Errors);
                    return RedirectToAction("Register", user);
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
            else {
                User user = new User {
                    Email = email,
                    Name = name,
                    UserName = email,
                };

                IdentityResult identResult = await userManager.CreateAsync(user);
                if (identResult.Succeeded) {
                    identResult = await userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded) {
                        await signInManager.SignInAsync(user, false);
                        EmailHelper emailHelper = new EmailHelper();
                        return RedirectToAction("Index", "Home");
                    }
                }
                // Access denied
                return View("/Views/Client/User/LoginClient.cshtml");
            }
        }

        [AllowAnonymous]
        public IActionResult Login() {
            var err = TempData["LoginError"] as string;
            if (err != null) {
                ViewData["LoginError"] = err;
            }
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

                    if (result.Succeeded == false) {
                        ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password");
                        TempData["LoginError"] = "Login Failed: Invalid Email or password";
                        return RedirectToAction("Login", login);
                    }

                    // Two Factor Authentication
                    if (result.RequiresTwoFactor) {
                        return RedirectToAction("LoginTwoStep", new { appUser.Email });
                    }

                    // Email confirmation 
                    bool emailStatus = await userManager.IsEmailConfirmedAsync(appUser);
                    if (emailStatus == false) {
                        ModelState.AddModelError(nameof(login.Email), "Email is unconfirmed, please confirm it first");
                        TempData["LoginError"] = "Email is unconfirmed, please confirm it first";
                    }

                    if (result.Succeeded) {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else {
                    ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email");
                    TempData["LoginError"] = "Login Failed: Invalid Email";
                }
            }
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(string email) {
            var user = await userManager.FindByEmailAsync(email);
            var token = await userManager.GenerateTwoFactorTokenAsync(user, "Email");

            HttpContext.Session.SetString("_Name", user.Name);
            HttpContext.Session.SetString("_Email", email);

            EmailHelper emailHelper = new EmailHelper();
            emailHelper.SendEmailTwoFactorCode(user.Email, token);

            return View("/Views/Client/User/LoginTwoStep.cshtml");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(TwoFactor twoFactor) {
            if (!ModelState.IsValid) {
                return View(twoFactor.TwoFactorCode);
            }
            var result = await signInManager.TwoFactorSignInAsync("Email", twoFactor.TwoFactorCode, false, false);
            // if code no valid return error
            // redirect to userInfo View (return user info JSON)
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else {
                // Remove session cookie of user login info
                HttpContext.Session.Remove("UserInfo.Session");
                Response.Cookies.Delete("UserInfo.Session");
                ModelState.AddModelError("", "Invalid Login Attempt");
                return View("/Views/Client/User/LoginTwoStep.cshtml");
            }
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword() {
            return View("/Views/Client/User/ForgotPassword.cshtml");
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required] string email) {
            if (!ModelState.IsValid)
                return View(email);

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "User", new { token, email = user.Email }, Request.Scheme);

            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = emailHelper.SendEmailPasswordReset(user.Email, link);

            if (emailResponse)
                return RedirectToAction("ForgotPasswordConfirmation");
            else {
                // log email failed 
            }
            return View(email);
        }
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation() {
            return View("/Views/Client/User/ForgotPasswordConfirmation.cshtml");
        }
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email) {
            var model = new ResetPassword { Token = token, Email = email };
            return View("/Views/Client/User/ResetPassword.cshtml", model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword) {
            if (!ModelState.IsValid)
                return View("/Views/Client/User/ResetPassword.cshtml", resetPassword);

            var user = await userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                RedirectToAction("ResetPasswordConfirmation");

            var resetPassResult = await userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!resetPassResult.Succeeded) {
                foreach (var error in resetPassResult.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return RedirectToAction("ResetPassword");
            }

            return RedirectToAction("ResetPasswordConfirmation");
        }

        public IActionResult ResetPasswordConfirmation() {
            return View("/Views/Client/User/ResetPasswordConfirmation.cshtml");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Detail() {
            string email = "binhvqse161554@fpt.edu.vn";
            User user = await userManager.FindByEmailAsync(email);
            EditUserViewModel editUser = new EditUserViewModel {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Name = user.Name,
                Password = user.PasswordHash,
                PhoneNumber = user.PhoneNumber
            };
            return View("/Views/Client/User/Detail.cshtml", editUser);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel editUser) {
            if (!ModelState.IsValid)
                return View("/Views/Client/User/Edit.cshtml", editUser);

            var user = await userManager.FindByEmailAsync(editUser.Email);
            if (user == null)
                RedirectToAction("Edit");

            return RedirectToAction("Edit");
        }
    }
}
