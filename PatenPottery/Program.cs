using Microsoft.EntityFrameworkCore;
using PatenPottery.Interface;
using PatenPottery.Models;
using PatenPottery.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// dependency injection start
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
// dependency injection end


builder.Services.AddDbContext<PatenPotteryContext>(options =>
{
    options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database = PatenPotteryDB; Trusted_Connection=True;");
});

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
