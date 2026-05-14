using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Mapping;
using WebApplication2.Repositories;
using WebApplication2.Services;

namespace WebApplication2;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("VehicleServiceCenterDb"));
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
        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}
