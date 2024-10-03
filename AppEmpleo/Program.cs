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

            builder.Services.AddDbContext<AppEmpleoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

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

            //app.MapGet("/", async context =>
            //{
            //    context.Response.Redirect("/LandingPage/Home");
            //});

            app.MapGet("/", async context =>
            {
                context.Response.Redirect("/Register/Register");
            });

            app.Run();
        }
    }
}
