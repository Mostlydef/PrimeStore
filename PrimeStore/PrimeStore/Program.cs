using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PrimeStore.Data;
using PrimeStore.Data.Interfaces;
using PrimeStore.Data.Repositiory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PrimeStoreContext>(options => options.UseNpgsql());
builder.Services.AddTransient<IAllFile, HomeRepository>();
var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStatusCodePages();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
