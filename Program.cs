using EmployeeApi.Infrastructure;
using EmployeeApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

//var builder = WebApplication.CreateBuilder(args);

//// Mssql repository
////var connection = builder.Configuration.GetConnectionString("Db");
////builder.Services.AddDbContext<EmployeesDbContext>(options => options.UseSqlServer(connection));

//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

namespace EmployeeApi.Infrastructure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //var server = builder.Configuration["Server"] ?? "db";
            //var port = builder.Configuration["Port"] ?? "1433";
            //var database = builder.Configuration["Database"] ?? "EmployeesDb";
            //var user = builder.Configuration["User"] ?? "admin";
            //var password = builder.Configuration["Password"] ?? "#123SuperSecure";
            //var connection = $"Server={server};Initial Catalog={database}; User ID ={user};Password={password};TrustServerCertificate=True;Integrated Security=false";

            //builder.Services.AddDbContext<EmployeesDbContext>(options =>
            //    options.UseSqlServer(connection));

            ConfigureServices(builder.Services);

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}