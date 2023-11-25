using Pharmacy_Project.Data;
using Pharmacy_Project;
using Pharmacy_Project.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
       

        string connectionString = builder.Configuration.GetConnectionString("MSSQL");
        builder.Services.AddDbContext<PharmacyContext>(option => option.UseSqlServer(connectionString));
        builder.Services.AddControllersWithViews(options => {
            options.CacheProfiles.Add("ModelCache",
                new CacheProfile()
                {
                    Location = ResponseCacheLocation.Any,
                    Duration = 2 * 3 + 240
                });
        });
        builder.Services.AddSession();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI()
                .AddDefaultTokenProviders();
        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();

        app.UseSession();

        app.UseDbInitializer();

        app.UseRouting();
        app.MapRazorPages();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");


        app.MapControllerRoute(
            name: "medicine",
            pattern: "{controller=Medicine}/{action=Index}");


        app.MapControllerRoute(
            name: "outgoing",
            pattern: "{controller=Outgoing}/{action=Index}");

        
        app.MapControllerRoute(
            name: "producer",
            pattern: "{controller=Producer}/{action=Index}");
        
        app.MapControllerRoute(
            name: "disease",
            pattern: "{controller=Disease}/{action=Index}");
        
        app.MapControllerRoute(
            name: "incoming",
            pattern: "{controller=Incoming}/{action=Index}");

        app.Run();
    }
}

