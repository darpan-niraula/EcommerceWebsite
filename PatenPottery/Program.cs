using Microsoft.EntityFrameworkCore;
using PatenPottery.Interface;
using PatenPottery.Models;
using PatenPottery.Service;
using Microsoft.AspNetCore.Identity;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var connectionString = builder.Configuration.GetConnectionString("PatenPotteryContextConnection") ?? throw new InvalidOperationException("Connection string 'PatenPotteryContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();


// dependency injection start
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
// dependency injection end


builder.Services.AddDbContext<PatenPotteryContext>(options =>
{
    options.UseSqlServer(connectionString);
});


builder.Services.AddDbContext<PatenPotteryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PatenPotteryContext>();

//builder.Services.AddDefaultIdentity<IdentityUser>()
//                .AddEntityFrameworkStores<PatenPotteryContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

// UseEndpoints configuration
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();
