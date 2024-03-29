using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;
using BethanysPieShop.App;
using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BethanysPieShopDbContextConnection") 
        ?? throw new InvalidOperationException("Connection string 'BethanysPieShopDbContextConnection' not found.");

// Adding services
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddRazorPages();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<BethanysPieShopDbContext>(options => {
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:BethanysPieShopDbContextConnection"]);
});

builder.Services.AddDefaultIdentity<IdentityUser>()
        .AddEntityFrameworkStores<BethanysPieShopDbContext>();


var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Adding middleware
app.UseStaticFiles();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

// Adding routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAntiforgery();

app.MapRazorPages();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Seed the database  
DbInitializer.Seed(app);

app.Run();
