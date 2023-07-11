
using EmployeeManagementMC.Core.Employees;
using EmployeeManagementMC.Repository;
using EmployeeManagementMC.Repository.Abstract;
using EmployeeManagementMC.Repository.Configuration;
using EmployeeManagementMC.Repository.Dapper;

namespace EmployeeManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register Services
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            // Register Repositories
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();


            // Register Configuration
            builder.Services.Configure<ConnectionStringsOptions>(builder.Configuration.GetSection("ConnectionStrings"));

            // Register Dapper
            builder.Services.AddTransient<IDapperContext, DapperContext>();


            //--------------------------------------------------------------------------//

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}