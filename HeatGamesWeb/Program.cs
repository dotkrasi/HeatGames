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
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HeatGamesDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    // Настройки на паролата
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<HeatGamesDbContext>()
.AddDefaultTokenProviders();

// ——— НОВО: СТЪПКА 1 (Добавяне на сесия в Services) ———
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2); // Пази количката 2 часа
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// —————————————————————————————————————————————————————
builder.Services.AddHttpContextAccessor();
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
        Console.WriteLine($"Грешка при сийдване на данни: {ex.Message}");
    }
}

/*using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Твоят стар сийдър за админа (не го пипай, ако го има)
    // await IdentitySeeder.SeedAdminAsync(services);

    // НОВОТО: Извикваме DbSeeder-а
    await HeatGames.Data.DbSeeder.SeedDataAsync(services); // Увери се, че пътят (namespace) е правилен!
}*/

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

// ——— НОВО: СТЪПКА 1 (Активиране на сесията) ———
// ВАЖНО: Трябва да е точно тук - между UseRouting и UseAuthorization!
app.UseSession();
// ——————————————————————————————————————————————

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();