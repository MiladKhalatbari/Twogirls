using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Stripe;
using TwoGirls.Core.Convertors;
using TwoGirls.Core.Services;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Context;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddElmahIo(o =>
         {
             o.ApiKey = "17ae4572c63e496aab3f53788f4e5917";
             o.LogId = new Guid("51fb103f-a0de-496b-ba82-d6c6077a205b");
         });

        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

        #region Stripe Gateway

        StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        #endregion

        #region Cookie Authentication

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        }).AddCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromDays(365 * 2);
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.HttpOnly = true;
        });

        #endregion

        #region DataBase Context

        builder.Services.AddDbContext<TwogirsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TwoGirs_DBconnectionString")));

        #endregion

        #region IoC

        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IPermissionService, PermissionService>();
        builder.Services.AddTransient<IProductService, TwoGirls.Core.Services.ProductService>();
        builder.Services.AddTransient<IOrderService, OrderService>();
        builder.Services.AddTransient<IRenderViewWithoutControllerToString, RenderViewWithoutControllerToString>();

        #endregion

        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        builder.Services.AddControllers();

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

        app.UseElmahIo();

        app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        app.Run();
    }
}

