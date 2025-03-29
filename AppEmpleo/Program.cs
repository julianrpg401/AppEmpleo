using AppEmpleo.Class.DataAccess;
using AppEmpleo.Class.Services;
using AppEmpleo.Interfaces;
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
            // Configuraci�n de Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File(@"C:\Users\Rpg40\source\repos\AppEmpleo\AppEmpleo\Logs\log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            // Agregar la cadena de conexi�n (configuraci�n de EF)
            builder.Services.AddDbContext<AppEmpleoContext>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

            // Autenticaci�n con claims
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Login"; // P�gina de inicio de sesi�n
                    options.AccessDeniedPath = "/AccessDenied"; // P�gina de acceso denegado
                });

            // Registrar repositorios y servicios
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ClaimsService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IOfferRepository, OfferRepository>();

            var app = builder.Build();

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

            app.UseAuthentication(); // Habilita la autenticaci�n
            app.UseAuthorization(); // Habilita la autorizaci�n

            app.MapRazorPages();

            try
            {
                Log.Information("Starting up the application");
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Error al iniciar la aplicaci�n.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
