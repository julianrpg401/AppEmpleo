using AppEmpleo.Class.DataAccess;
using AppEmpleo.Class.Services;
using AppEmpleo.Class.Services.SessionServices;
using AppEmpleo.Class.Middleware;
using AppEmpleo.Interfaces.Repositories;
using AppEmpleo.Interfaces.Services;
using AppEmpleo.Interfaces.Services.SessionServices;
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
            // Configuración de Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File(@"C:\Users\Rpg40\source\repos\AppEmpleo\AppEmpleo\Logs\log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            // Agregar la cadena de conexión (configuración de EF)
            builder.Services.AddDbContext<AppEmpleoContext>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

            // Autenticación con claims
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Login"; // Página de inicio de sesión
                    options.AccessDeniedPath = "/AccessDenied"; // Página de acceso denegado
                });

            // Registrar repositorios y servicios
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IOfferService, OfferService>();
            builder.Services.AddScoped<IPostulationService, PostulationService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IClaimsService, ClaimsService>();
            builder.Services.AddScoped<IOfferRepository, OfferRepository>();
            builder.Services.AddScoped<IPostulationRepository, PostulationRepository>();

            var app = builder.Build();

            // Middleware global de manejo de excepciones
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

            app.UseAuthentication(); // Habilita la autenticación
            app.UseAuthorization(); // Habilita la autorización

            app.MapRazorPages();

            try
            {
                Log.Information("Starting up the application");
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Error al iniciar la aplicación.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
