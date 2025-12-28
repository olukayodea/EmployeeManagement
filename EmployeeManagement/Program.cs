using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
namespace EmployeeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddDbContext<Data.AppDbContext>(options =>
                options.UseInMemoryDatabase("EmployeeDb"));

            // Configure CORS to allow requests from any origin
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            // Build the app
            var app = builder.Build();
            app.UseCors("AllowAll");

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
