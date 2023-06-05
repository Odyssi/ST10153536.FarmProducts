using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROG7311.FarmProducts.ST10153536.Models.Domain;
using PROG7311.FarmProducts.ST10153536.ViewModels;

namespace PROG7311.FarmProducts.ST10153536.Controllers
{
    
    [Authorize(Roles = "Admin")] // Only users with the "Admin" role can access this controller
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole { Name = model.Name };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("AdminIndex", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Employee")] // Only users with the "Admin" or "Employee" roles can access this action
        public async Task<IActionResult> Edit(string id)
        {
            var currentUser = await _userManager.GetUserAsync(User); // Get the current user
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                // Check if the current user has the necessary authorization to edit the role
                if (User.IsInRole("Admin") || (User.IsInRole("Employee") && role.Name == "Farmers"))
                {
                    var model = new RoleViewModel { Id = role.Id, Name = role.Name };
                    return View(model);
                }
            }
            return RedirectToAction("ProductIndex", "Home");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User); // Get the current user
                var role = await _roleManager.FindByIdAsync(model.Id);
                if (role != null)
                {
                    // Check if the current user has the necessary authorization to edit the role
                    if (User.IsInRole("Admin") || (User.IsInRole("Employee") && role.Name == "Farmers" && currentUser.Id == model.Id))
                    {
                        role.Name = model.Name;
                        var result = await _roleManager.UpdateAsync(role);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("FarmerIndex", "FarmerDashboard");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
            }
            return View("EmployeeDashboard","AdminDashboard");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("EmployeeIndex", "FarmerIndex");
                }
                else
                {
                    // Handle the error case
                }
            }
            return RedirectToAction("AdminIndex", "AdminDashboard");
        }
    }
}