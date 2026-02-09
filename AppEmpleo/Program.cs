using AppEmpleo.Class.DataAccess;
using AppEmpleo.Class.Middleware;
using AppEmpleo.Class.Security;
using AppEmpleo.Class.Services;
using AppEmpleo.Class.Services.SessionServices;
using AppEmpleo.Class.Utilities.Normalization;
using AppEmpleo.Class.Utilities.Storage;
using AppEmpleo.Interfaces.Repositories;
using AppEmpleo.Interfaces.Security;
using AppEmpleo.Interfaces.Services;
using AppEmpleo.Interfaces.Services.SessionServices;
using AppEmpleo.Interfaces.Services.Storage;
using AppEmpleo.Interfaces.Utilities;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AppEmpleo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext();
            });

            // Add services to the container.
            builder.Services.AddRazorPages();

            // Add the database connection (EF configuration).
            builder.Services.AddDbContext<AppEmpleoContext>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

            // Authentication with claims.
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Login";
                    options.AccessDeniedPath = "/AccessDenied";
                    options.Cookie.Name = "AppEmpleo.Auth";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                });

            // Register repositories and services.
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IOfferService, OfferService>();
            builder.Services.AddScoped<IJobApplicationService, JobApplicationService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IClaimsService, ClaimsService>();
            builder.Services.AddScoped<IOfferRepository, OfferRepository>();
            builder.Services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IUserNormalizer, UserNormalizer>();
            builder.Services.AddScoped<IOfferNormalizer, OfferNormalizer>();
            builder.Services.AddScoped<IResumeFactory, ResumeFactory>();
            builder.Services.AddScoped<IResumeStorage, FileSystemResumeStorage>();

            var app = builder.Build();

            // Global exception handling middleware.
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
