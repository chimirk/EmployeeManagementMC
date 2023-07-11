using EmployeeManagementMC.Repository.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EmployeeManagementMC.Repository.Dapper
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly ConnectionStringsOptions _connectionStringsOptions;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DapperConnectionString");
           
        }

        public IDbConnection CreateConnection()
        {
            return new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
        }
    }
}
