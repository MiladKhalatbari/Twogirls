using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using twoGirlsOnlineShop.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
#region DB
builder.Services.AddDbContext<TwogirsContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("TwoGirs_DBconnectionString"))
);
#endregion
#region Add Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option => {
    option.ExpireTimeSpan = TimeSpan.FromDays(365*2);
    option.LoginPath = "/Account/Login";
     option. LogoutPath = "/Account/Logout";
    }
); 
#endregion 
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
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/Admin")|| context.Request.Path.StartsWithSegments("/ManageUsers"))
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Response.Redirect("/Account/Login");
        }
        else if(!bool.Parse(context.User.FindFirstValue("IsAdmin")))
        {
           context.Response.Redirect("/Account/Login");
        }
    }
       await next.Invoke();

}); app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
