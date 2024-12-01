using AppEmpleo.Models;
using Microsoft.EntityFrameworkCore;

namespace AppEmpleo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Agregar consola
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // Add services to the container.
            builder.Services.AddRazorPages();

            // Registrar IHttpContextAccessor
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Agregar servicio
            builder.Services.AddDistributedMemoryCache(); // Para manejar sesiones en memoria
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
                options.Cookie.HttpOnly = true; // Protección contra scripts maliciosos
                options.Cookie.IsEssential = true; // Requerido para políticas de consentimiento
            });

            builder.Services.AddDbContext<AppEmpleoContext>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

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

            app.UseSession();
            
            app.UseAuthorization();

            app.MapRazorPages();

            //app.MapGet("/", async context =>
            //{
            //    context.Response.Redirect("/LandingPage/Home");
            //});

            //app.MapGet("/", async context =>
            //{
            //    context.Response.Redirect("/Register/CreateAccount");
            //});

            app.Run();
        }
    }
}
