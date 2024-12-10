using AppEmpleo.Class;
using AppEmpleo.Models;
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
            builder.Services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", options =>
                {
                    options.LoginPath = "/Login"; // Página de login
                    options.AccessDeniedPath = "/AccessDenied"; // Página para usuarios sin acceso
                });

            // Registrar repositorios y servicios
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ClaimsService>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<OfferRepository>();

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
            
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
