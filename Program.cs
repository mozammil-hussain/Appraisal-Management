using Microsoft.EntityFrameworkCore;

using System.Data.Common;
using System.Data;
using AppraisalManagentSystem.Data;
using AppraisalManagentSystem.Interfaces;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using AppraisalManagentSystem.InterfaceImpl;
using AppraisalManagentSystem.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IEmployeeOperations, EmployeeOperations>();
builder.Services.AddTransient<ICompetencies,CompetenciesImpl>();
builder.Services.AddTransient<ILoginUser, Login>();
builder.Services.AddTransient<IAppraisalServices, AppraisalService>();
builder.Services.AddScoped<ILoginAuthenticationService, LoginAuthenticationService>();




//builder.Services.AddTransient<IEmailService, EmailServiceImpl>();

builder.Services.AddDbContext<Db>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




//string con = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<DbConnection>(builder => { builder.UseSqlServer(con).EnableSensitiveDataLogging(); ; });



builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RequireHRRole", policy =>
//              policy.RequireRole("HR"));

//});




builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.LoginPath = "/Home/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        option.AccessDeniedPath = "/Error/AccessDenied";

    });



//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("HR"));
//    options.AddPolicy("RequireLoggedIn", policy => policy.RequireAuthenticatedUser());
//});

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

app.UseAuthorization();
app.UseAuthentication();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();