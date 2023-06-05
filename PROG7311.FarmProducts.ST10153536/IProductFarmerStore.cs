using Microsoft.AspNetCore.Identity;
using PROG7311.FarmProducts.ST10153536.Data;
using PROG7311.FarmProducts.ST10153536.Models;
using PROG7311.FarmProducts.ST10153536.Models.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public interface IProductFarmerStore
{
    Task AddProductToFarmerAsync(IdentityRole farmerRole, Product product, CancellationToken cancellationToken);
    Task RemoveProductFromFarmerAsync(IdentityRole farmerRole, Product product, CancellationToken cancellationToken);
    Task<List<Product>> GetProductsForFarmerAsync(IdentityRole farmerRole, CancellationToken cancellationToken);
}

public class CustomProductFarmerStore : IProductFarmerStore
{
    private readonly PROG7311FarmProductsST10153536Context _context;

    public CustomProductFarmerStore(PROG7311FarmProductsST10153536Context context)
    {
        _context = context;
    }

    public async Task AddProductToFarmerAsync(IdentityRole farmerRole, Product product, CancellationToken cancellationToken)
    {
        var farmerId = farmerRole.Id;
        var farmerProduct = new ProductFarmer { ApplicationUserId = farmerId, ProductId = product.Id };
        _context.ProductFarmers.Add(farmerProduct);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveProductFromFarmerAsync(IdentityRole farmerRole, Product product, CancellationToken cancellationToken)
    {
        var farmerId = farmerRole.Id;
        var farmerProduct = await _context.ProductFarmers.FirstOrDefaultAsync(fp => fp.ApplicationUserId == farmerId && fp.ProductId == product.Id);

        if (farmerProduct != null)
        {
            _context.ProductFarmers.Remove(farmerProduct);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<List<Product>> GetProductsForFarmerAsync(IdentityRole farmerRole, CancellationToken cancellationToken)
    {
        var farmerId = farmerRole.Id;

        var productIds = await _context.ProductFarmers
            .Where(fp => fp.ApplicationUserId == farmerId)
            .Select(fp => fp.ProductId)
            .ToListAsync(cancellationToken);

        var products = await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        return products;
    }
}
