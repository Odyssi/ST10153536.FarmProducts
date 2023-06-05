
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PROG7311.FarmProducts.ST10153536.Areas.Identity.Data;
using PROG7311.FarmProducts.ST10153536.Data;
using PROG7311.FarmProducts.ST10153536.Models.Domain;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PROG7311FarmProductsST10153536ContextConnection") ?? throw new InvalidOperationException("Connection string 'PROG7311FarmProductsST10153536ContextConnection' not found.");

builder.Services.AddDbContext<PROG7311FarmProductsST10153536Context>(options => options.UseSqlServer(connectionString));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddDefaultUI()
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddEntityFrameworkStores<PROG7311FarmProductsST10153536Context>()
    .AddUserStore<CustomUserStore>()
    .AddDefaultTokenProviders()
    .Services.AddScoped<UserManager<ApplicationUser>>();

builder.Services.AddScoped<IProductFarmerStore, CustomProductFarmerStore>();

    
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "identity",
    pattern: "{area=Identity}/{controller=Account}/{action=Login}/{id?}");

app.Run();