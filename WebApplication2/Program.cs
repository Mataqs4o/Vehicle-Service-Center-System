using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Hubs;
using WebApplication2.Mapping;
using WebApplication2.Models;
using WebApplication2.Repositories;
using WebApplication2.Services;

namespace WebApplication2;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=vehicle-service-center.db"));
        builder.Services.AddSignalR();
        builder.Services.AddAutoMapper(typeof(MappingProfile));

        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<IServiceRecordService, ServiceRecordService>();
        builder.Services.AddScoped<IEmailNotificationService, ConsoleEmailNotificationService>();

        builder.Services.AddAuthentication("Cookies").AddCookie("Cookies");
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Administrator"));
            options.AddPolicy("MechanicOnly", policy => policy.RequireRole("Mechanic"));
            options.AddPolicy("CustomerOnly", policy => policy.RequireRole("Customer"));
        });

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapHub<PublicRecordsHub>("/hubs/public-records");

        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.EnsureCreated();
        }

        SeedDemoData(app);

        app.Run();
    }

    private static void SeedDemoData(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (dbContext.Customers.Any()) return;

        var customer = new Customer
        {
            FirstName = "Alex",
            LastName = "Driver",
            Email = "customer@example.com"
        };

        dbContext.Customers.Add(customer);
        dbContext.SaveChanges();

        dbContext.Vehicles.AddRange(
            new Vehicle { Make = "Toyota", Model = "Camry", Year = 2022, CustomerId = customer.CustomerId },
            new Vehicle { Make = "Ford", Model = "F-150", Year = 2021, CustomerId = customer.CustomerId },
            new Vehicle { Make = "Honda", Model = "Civic", Year = 2023, CustomerId = customer.CustomerId }
        );

        dbContext.SaveChanges();
    }
}
