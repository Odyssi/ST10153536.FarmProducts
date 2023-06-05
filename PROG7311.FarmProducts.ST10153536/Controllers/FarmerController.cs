using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG7311.FarmProducts.ST10153536.Data;
using PROG7311.FarmProducts.ST10153536.Models;
using PROG7311.FarmProducts.ST10153536.Models.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace PROG7311.FarmProducts.ST10153536.Controllers
{
    [Authorize(Roles = "Farmer")] // Restrict access to users with the "Farmer" role
    public class FarmerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PROG7311FarmProductsST10153536Context _context;

        public FarmerController(UserManager<ApplicationUser> userManager, PROG7311FarmProductsST10153536Context context)
        {
            _userManager = userManager;
            _context = context;
        }

        // Action to display the farmer's products
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            var products = await _context.ProductFarmers
                .Include(pf => pf.Product)
                .Where(pf => pf.ApplicationUserId == userId)
                .Select(pf => pf.Product)
                .ToListAsync();

            return View(products);
        }

        // Action to add a new product to the farmer's profile
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductFarmer product)
        {
            var userId = GetCurrentUserId();
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Farmer"))
            {
                return Unauthorized();
            }

            _context.ProductFarmers.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Action to delete a product from the farmer's profile
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var userId = GetCurrentUserId();
            var product = await _context.ProductFarmers
                .FirstOrDefaultAsync(pf => pf.ProductId == productId && pf.ApplicationUserId == userId);

            if (product == null)
            {
                return NotFound();
            }

            _context.ProductFarmers.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Helper method to get the currently logged-in user's ID
        private string GetCurrentUserId()
        {
            return _userManager.GetUserId(User);
        }
    }
}
