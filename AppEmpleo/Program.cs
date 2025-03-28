using AppEmpleo.Class.DataAccess;
using AppEmpleo.Class.Services;
using AppEmpleo.Interfaces;
using AppEmpleo.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace AppEmpleo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Agregar consola para depurar código
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

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

            app.UseAuthentication(); // Habilita la autenticación
            app.UseAuthorization(); // Habilita la autorización

            app.MapRazorPages();

            app.Run();
        }
    }
}
