using EmployeeApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

//var connection = builder.Configuration.GetConnectionString("Db");
//builder.Services.AddDbContext<EmployeesDbContext>(options => options.UseSqlServer(connection));

namespace EmployeeApi.Infrastructure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var server = builder.Configuration["Server"] ?? "db";
            var port = builder.Configuration["Port"] ?? "1433";
            var database = builder.Configuration["Database"] ?? "EmployeesDb";
            var user = builder.Configuration["User"] ?? "SAA";
            var password = builder.Configuration["Password"] ?? "#123SuperSecure";
            var connection = $"Server={server},{port};Initial Catalog={database}; User ID ={user};Password={password};TrustServerCertificate=True;Integrated Security=false";
            //var connection = $"Server={server};Initial Catalog={database}; User ID ={user};Password={password};TrustServerCertificate=True;Integrated Security=false";

            builder.Services.AddDbContext<EmployeesDbContext>(options =>
                options.UseSqlServer(connection));

            ConfigureServices(builder.Services);

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            DatabaseSetup.StartDatabase(app);

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<RoleService>();
            services.AddScoped<EmployeeService>();
        }
    }
}