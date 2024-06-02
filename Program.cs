using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SportClubs1.Areas.Identity.Pages.Account;
using SportClubs1.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<SportClubsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SportClubsContext")));

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationIdentityContext>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationIdentityContext>();

builder.Services
 .AddDbContext<ApplicationIdentityContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationIdentityContext"), sqlOptions =>
    sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name)));



builder.Services.AddTransient<IEmailSender, EmailSender>();


// Add services to the container.
var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    await app.InitializeRolesAsync();
    await app.InitializeDefaultUsersAsync(
        app.Configuration.GetSection("IdentityDefaults: Admin"),
        app.Configuration.GetSection("IdentityDefaults: Client"),
        app.Configuration.GetSection("IdentityDefaults: Staff"));
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

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
