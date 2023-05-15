using WebLibraryApplication1;
using Microsoft.EntityFrameworkCore;
using WebLibraryApplication1.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Cookies";
    options.DefaultSignInScheme = "Cookies";
    options.DefaultChallengeScheme = "Cookies";
})
.AddCookie("Cookies", options =>
{
    options.LoginPath = "/Login/Index";
    options.AccessDeniedPath = "/Login/Index";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
    options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
    options.Cookie.Name = "YourCookieName";
    options.Cookie.Path = "/";
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthorization();

builder.Services.AddDbContext<DblibraryContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

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

//app.UseCookiePolicy(new CookiePolicyOptions
//{
//    MinimumSameSitePolicy = SameSiteMode.Strict,
//    OnAppendCookie = cookieContext =>
//    {
//        cookieContext.CookieOptions.Expires = DateTimeOffset.Now.AddDays(1);
//    },
//    OnDeleteCookie = cookieContext =>
//    {
//        cookieContext.CookieOptions.Expires = DateTimeOffset.Now.AddDays(-1);
//    }
//});

app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
