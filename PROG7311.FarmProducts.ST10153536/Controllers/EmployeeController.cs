using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using PROG7311.FarmProducts.ST10153536.Models.Domain;
using System.Linq;
using System.Threading.Tasks;
using PROG7311.FarmProducts.ST10153536.Models;
using PROG7311.FarmProducts.ST10153536.Data;

namespace PROG7311.FarmProducts.ST10153536.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly PROG7311FarmProductsST10153536Context _context;

        public EmployeeController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, PROG7311FarmProductsST10153536Context context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var farmers = await _userManager.GetUsersInRoleAsync("Farmer");
            return View(farmers);
        }

        public IActionResult AddFarmer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFarmer(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await _userManager.CreateAsync(user, model.PasswordHash);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Farmer");
                    // Farmer added successfully
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var isEmployeeRoleExists = await _roleManager.RoleExistsAsync("Employee");
                if (isEmployeeRoleExists)
                {
                    await _userManager.RemoveFromRoleAsync(user, "Employee");
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ProductsByFarmer(string userId)
        {
            var farmer = await _userManager.FindByIdAsync(userId);
            if (farmer == null)
            {
                return NotFound();
            }

            var products = await _context.ProductFarmers
                .Include(pf => pf.Product)
                .Where(pf => pf.ApplicationUserId == farmer.Id)
                .Select(pf => pf.Product)
                .ToListAsync();

            return View(products);
        }
    }
}
