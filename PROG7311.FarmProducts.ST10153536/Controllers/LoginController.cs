using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG7311.FarmProducts.ST10153536.Areas.Identity.Pages.Account;
using PROG7311.FarmProducts.ST10153536.Models.Domain;
using PROG7311.FarmProducts.ST10153536.Models.ViewModels;

namespace PROG7311.FarmProducts.ST10153536.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
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
                            return RedirectToAction("AdminDashboard", "AdminIndex");
                        }
                        else if (roles.Contains("Farmer"))
                        {
                            return RedirectToAction("FarmerDashboard", "FarmerIndex");
                        }
                        else if (roles.Contains("Employee"))
                        {
                            return RedirectToAction("EmployeeDashboard", "EmployeeIndex");
                        }
                    }

                    // Redirect to a default dashboard if the user role is not recognized
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                }
            }

            // If we reach this point, login failed, so redisplay the login page with validation errors
            return View("Layout");
        }
    }
}