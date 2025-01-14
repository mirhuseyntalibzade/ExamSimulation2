using BL.Profiles;
using BL.Services.Abstractions;
using BL.Services.Concretes;
using DAL.Contexts;
using DAL.Repositories.Abstractions;
using DAL.Repositories.Concretes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"));
    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});


builder.Services.AddScoped<ITechnicianRepository, TechnicianRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceModelService, ServiceModelService>();
builder.Services.AddScoped<ITechnicianService, TechnicianService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAutoMapper(typeof(ServiceProfile));
builder.Services.AddAutoMapper(typeof(TechnicianProfile));
builder.Services.AddAutoMapper(typeof(IdentityUserProfile));

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    opt.AddPolicy("User", policy => policy.RequireRole("User"));
});

var app = builder.Build();

using (var scoped = app.Services.CreateScope())
{
    var services = scoped.ServiceProvider;
    try
    {
        await DatabaseSeeder.SeedDataAsync(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Couldnt seed database.");
    };
}


app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
        pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
