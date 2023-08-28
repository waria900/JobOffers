using Domains;
using Bl;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// SQL StringConnection \\
builder.Services.AddDbContext<JobOffersContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));





// -- Microsoft.AspNetCore.Identity.EntityFrameworkCore --

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Search on these on Google:

    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.User.RequireUniqueEmail = true;

    //options.SignIn.RequireConfirmedPhoneNumber = true;

    // To conntect (Microsoft Identitiy) with (Entity Framewrok).
}).AddEntityFrameworkStores<JobOffersContext>();


// -- ReturnUrl --
// Means you can not reach to any page by writing its path in URL if you are not loged in.
// [Authorize]: it must be written about view function to restrict reaching to that page before login.

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Users/AccessDenied";
    options.Cookie.Name = "Cookie";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
    // will send name of path page to the controller for example (Order/OrderSuccess).
    options.LoginPath = "/Users/Login";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});


// Dependency Injection \\
builder.Services.AddScoped<IJobs,ClsJobs>();
builder.Services.AddScoped< IjobLocations,ClsJobLocations>();
builder.Services.AddScoped<IJobTypes, ClsJobTypes>();
builder.Services.AddScoped<ICategories, ClsCategories>();
builder.Services.AddScoped<IApplyForJob, ClsApplyForJob>();


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

app.UseEndpoints(endpoints =>
{

    endpoints.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}");

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


    endpoints.MapControllerRoute(
    name: "ali",
    pattern: "ali/{controller=Home}/{action=Index}/{id?}");

});

app.Run();
