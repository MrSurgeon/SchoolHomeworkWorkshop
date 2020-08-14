using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCompany.School.HomeworkDemo.Security;
using MyCompany.School.HomeworkDemo.Models.Security;
using System;
using System.Threading.Tasks;
using System.Web;

namespace MyCompany.School.HomeworkDemo.Controllers
{
    public class SecurityController : Controller
    {
        
        private UserManager<SchoolUser> _userManager;
        private SignInManager<SchoolUser> _signInManager;
       
        public SecurityController(UserManager<SchoolUser> userManager,
            SignInManager<SchoolUser> signInManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        //GET :Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //POST: Login
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
            if (user != null)
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError(string.Empty, "Confirm Your E-Mail Please");
                    return View(loginViewModel);
                }
            }
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Username or Password is Failed");
            return View(loginViewModel);
        }

        [Authorize]
        //GET: Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //GET: AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = new SchoolUser
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
            };

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (result.Succeeded)
            {
                var confirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var callBackUrl = Url.Action("ConfirmEmail", "Security",
                                            new
                                            {
                                                id = user.Id,
                                                code = HttpUtility.UrlEncode(confirmationCode),

                                            }, protocol: Request.Scheme);
                // Send-Email Codes

                return RedirectToAction("Index", "Home");
            }

            return View(registerViewModel);
        }

        public async Task<IActionResult> ConfirmEmail(string id, string code)
        {
            if (id == null || code == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _userManager.FindByIdAsync(id);
            //
            if (user == null)
            {
                throw new ApplicationException("Unable to find user");
            }
            var result = await _userManager.ConfirmEmailAsync(user, HttpUtility.UrlDecode(code));

            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return View();
            }

            var confirmationCode = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callBackUrl = Url.Action("ResetPassword", "Security",
                                            new
                                            {
                                                id = user.Id,
                                                code = HttpUtility.UrlEncode(confirmationCode)
                                            });

            // Send CallbackUrl with email

            return RedirectToAction("ForgotPasswordEmailSent");

        }

        public IActionResult ForgotPasswordEmailSent()
        {
            return View();
        }

        public IActionResult ResetPassword(string id, string code)
        {
            if (id == null || code == null)
            {
                throw new ApplicationException("Code must be supplied for password reset");
            }

            var model = new ResetPasswordViewModel
            {
                Code = code
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordViewModel);
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);

            if (user == null)
            {
                throw new ApplicationException("User Not Found");
            }
            var result = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(resetPasswordViewModel.Code),
                resetPasswordViewModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirm");
            }

            return View(resetPasswordViewModel);
        }

        public IActionResult ResetPasswordConfirm()
        {
            return View();
        }
    }
}
