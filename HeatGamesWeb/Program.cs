using HeatGames.Core.Services;
using HeatGames.Core.Services.Interfaces;
using HeatGames.Data;
using HeatGames.Data.Configuration;
using HeatGames.Data.Models;
using HeatGamesCore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IDeveloperService, DeveloperService>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HeatGamesDbContext>(options =>
    options.UseSqlServer(connectionString));
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await IdentitySeeder.SeedRolesAndAdminAsync(services);
    }
    catch (Exception ex)
    {
        // ??????? ????????, ??? ???? ?????? ?? ????? ?? ??????????
        Console.WriteLine($"?????? ??? ???????? ?? ?????: {ex.Message}");
    }
}

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
