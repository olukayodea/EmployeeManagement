using EmployeeManagement.Data;
using EmployeeManagement.Repositories;
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
                options.UseSqlServer(
                    "Data Source=OLUKAYODE\\SQLEXPRESS;" +
                    "Database=Employees;" +
                    "Integrated Security=True;" +
                    "Persist Security Info=False;" +
                    "Pooling=False;" +
                    "MultipleActiveResultSets=False;" +
                    "Encrypt=True;" +
                    "TrustServerCertificate=True"
                    ));
            //options.UseInMemoryDatabase("EmployeeDb"));

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

            // Register the EmployeeRepository for dependency injection
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            // Add controllers
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Build the app
            var app = builder.Build();
            // Enable Swagger in development environment
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI( c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            app.UseCors("AllowAll");
            app.MapControllers();

            app.Run();
        }
    }
}
