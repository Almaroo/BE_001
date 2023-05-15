using BE_001.Web.DbContexts;
using BE_001.Web.Helpers;
using BE_001.Web.Models;
using BE_001.Web.Repositories.Items;
using BE_001.Web.Repositories.ReviewRepository;
using BE_001.Web.Repositories.Reviews;
using BE_001.Web.Services.Browsing;
using BE_001.Web.Services.Cart;
using BE_001.Web.Services.Rating;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ShopDb");
builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IRatingService, RatingService>();

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IBrowsingService, BrowsingService>();

builder.Services.AddScoped<ICartService, SessionCartService>();

builder.Services.AddIdentity<User, IdentityRole>(
        options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ShopDbContext>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MigrateDatabase();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();