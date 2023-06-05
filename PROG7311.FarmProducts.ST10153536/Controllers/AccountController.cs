using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG7311.FarmProducts.ST10153536.Models;
using PROG7311.FarmProducts.ST10153536.Models.Domain;
using PROG7311.FarmProducts.ST10153536.Models.ViewModels;
using PROG7311.FarmProducts.ST10153536.ViewModels;

namespace PROG7311.FarmProducts.ST10153536.Controllers
{
 
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _signInManager.UserManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        var roles = await _signInManager.UserManager.GetRolesAsync(user);

                        if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("AdminDashboard", "Home");
                        }
                        else if (roles.Contains("Farmer"))
                        {
                            return RedirectToAction("FarmerDashboard", "Home");
                        }
                        else if (roles.Contains("Employee"))
                        {
                            return RedirectToAction("EmployeeDashboard", "Home");
                        }
                    }

                    // Redirect to a default dashboard if the user role is not recognized
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View("Login", model);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Set the user's role based on the selected role during registration
                    var roleName = model.Roles.ToString();
                    await _userManager.AddToRoleAsync(user, roleName);

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // Redirect the user based on their role
                    if (roleName == "Admin")
                    {
                        return RedirectToAction("Index", "Employee", "Farmer", "Product");
                    }
                    else if (roleName == "Employee")
                    {
                        return RedirectToAction("Index", "Farmer", "ProductFarmer");
                    }
                    else if (roleName == "Farmer")
                    {
                        return RedirectToAction("Index", "Farmer", "ProductFarmer");
                    }
                    else
                    {
               
                        return RedirectToAction("Index", "Home");
                    }
                }
                AddErrors(result);
            }


            return View("Register");
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Layout");
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
