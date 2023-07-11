using Dapper;
using EmployeeManagementMC.Repository.Abstract;
using EmployeeManagementMC.Repository.Configuration;
using EmployeeManagementMC.Repository.Common;
using EmployeeManagementMC.Repository.Models;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using EmployeeManagementMC.Repository.Dapper;

namespace EmployeeManagementMC.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ConnectionStringsOptions _connectionStringsOptions;
        private readonly IDapperContext _dapperContext;

        public EmployeeRepository(IOptions<ConnectionStringsOptions> connectionStringsOptions, IDapperContext context)
        {
            _connectionStringsOptions = connectionStringsOptions.Value;
            _dapperContext = context;
        }
        public async Task<EmployeeInsertResponseModel> AddNewEmployeeRecordAsync(EmployeeInsertRequestModel request)
        {
            using (var connection = new SqlConnection(_connectionStringsOptions.AdoConnectionString))
            {
                connection.Open();

                string sqlQuery = @"DECLARE @InsertedIds TABLE (Id UNIQUEIDENTIFIER)
                                INSERT INTO Employee (Name, Position)
                                OUTPUT inserted.Id INTO @InsertedIds
                                VALUES (@NameParam, @PositionParam)
                                SELECT Id AS LastInsertedId FROM @InsertedIds";

                EmployeeInsertResponseModel insertedEmployee = new EmployeeInsertResponseModel();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sqlQuery;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@NameParam", request.Name);
                    command.Parameters.AddWithValue("@PositionParam", request.Position);

                    // retrieve newly inserted employee
                    Guid id = (Guid)await command.ExecuteScalarAsync();

                    //if (ObjectHelper.IsNotNull(id)){
                    if (id.IsNotNull())
                    {
                        insertedEmployee.Id = Guid.Parse(id.ToString());
                        insertedEmployee.Name = request.Name;
                        insertedEmployee.Position = request.Position;
                        insertedEmployee.ManagerId = request.ManagerId;
                        insertedEmployee.AddressId = request.AddressId;
                    }
                    else
                    {
                        throw new Exception("SQL Error. An error occured while inserting new Employee Record");
                    }
                }

                return insertedEmployee;
            }
        }

        public async Task DeleteEmployeeRecordAsync(Guid id)
        {
            using (var connection = new SqlConnection(_connectionStringsOptions.AdoConnectionString))
            {
                connection.Open();

                string sqlQuery = @"DELETE FROM Employee WHERE Id = @IdParam";

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sqlQuery;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@IdParam", $"{id}");

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IList<EmployeeLookupResponseModel>> EmployeesLookupAsync(EmployeeLookupRequestModel request)
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionStringsOptions.AdoConnectionString;
                connection.Open();
                var command = BuildEmployeeLookupQueryCommand(request);
                command.Connection = connection;
                var reader = await command.ExecuteReaderAsync();

                var employees = new List<EmployeeLookupResponseModel>();
                while (reader.Read())
                {
                    var employee = new EmployeeLookupResponseModel
                    {
                        Id = reader.GetGuid("Id"),
                        Name = reader.GetString("Name")
                    };

                    if (!reader.IsDBNull("Position"))
                        employee.Position = reader.GetString("Position");

                    if (!reader.IsDBNull("ManagerId"))
                        employee.ManagerId = reader.GetGuid("ManagerId");

                    if (!reader.IsDBNull("ManagerName"))
                    {
                        employee.ManagerName = reader.GetString("ManagerName");
                    }

                    if (!reader.IsDBNull("City"))
                    {
                        employee.City = reader.GetString("City");
                    }

                    if (!reader.IsDBNull("StreetName"))
                    {
                        employee.StreetName = reader.GetString("StreetName");
                    }

                    if (!reader.IsDBNull("StreetNumber"))
                    {
                        employee.StreetNumber = reader.GetString("StreetNumber");
                    }

                    if (!reader.IsDBNull("EmployeesUnderManagement"))
                    {
                        employee.EmployeesUnderManagement = reader.GetInt32("EmployeesUnderManagement");
                    }

                    employees.Add(employee);
                }

                return employees;

            }
        }

        public async Task<EmployeeLookupResponseModel> GetEmployeeByIdAsync(Guid id)
        {
            // Throwing exceptions on purpose example.
            if (id == Guid.Empty)
                throw new ApplicationException("This was an exception thrown on purpose.");

            var query = BuildGetEmployeeByIDQuery();
            var parameters = new DynamicParameters();
            parameters.Add("IdParam", id, DbType.Guid);

            // using Dapper to get Employee Info by a specific Id
            using (var connection = _dapperContext.CreateConnection())
            {
                var employee = await connection.QuerySingleOrDefaultAsync<EmployeeLookupResponseModel>(query, parameters);
                return employee;
            }

            /*using (var connection = new SqlConnection(_connectionStringsOptions.DapperConnectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = BuildGetEmployeeByIDQuery();
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@IdParam", $"{id}");

                    var employee = new EmployeeLookupResponseModel();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull("Id")) { employee.Id = reader.GetGuid("Id"); }
                            if (!reader.IsDBNull("Name")) { employee.Name = reader.GetString("Name"); }
                            if (!reader.IsDBNull("Position")) { employee.Position = reader.GetString("Position"); }
                            if (!reader.IsDBNull("ManagerId")) { employee.ManagerId = reader.GetGuid("ManagerId"); }
                            if (!reader.IsDBNull("ManagerName")) { employee.ManagerName = reader.GetString("ManagerName"); }
                            if (!reader.IsDBNull("City")) { employee.City = reader.GetString("City"); }
                            if (!reader.IsDBNull("StreetName")) { employee.StreetName = reader.GetString("StreetName"); }
                            if (!reader.IsDBNull("StreetNumber")) { employee.StreetNumber = reader.GetString("StreetNumber"); }
                            if (!reader.IsDBNull("EmployeesUnderManagement")) { employee.EmployeesUnderManagement = reader.GetInt32("EmployeesUnderManagement"); }
                        }
                    }
                    return employee;
                }
            }*/
        }

        public async Task<EmployeeInsertResponseModel> UpdateEmployeeRecordAsync(Guid id, EmployeeInsertRequestModel request)
        {
            using (var connection = new SqlConnection(_connectionStringsOptions.AdoConnectionString))
            {
                connection.Open();

                string sqlQuery = @"UPDATE Employee SET Name = @NameParam, Position = @PositionParam WHERE Id = @IdParam";

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sqlQuery;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@NameParam", request.Name);
                    command.Parameters.AddWithValue("@PositionParam", request.Position);
                    command.Parameters.AddWithValue("@IdParam", $"{id}");

                    await command.ExecuteNonQueryAsync();
                }

                return new EmployeeInsertResponseModel();
            }
        }



        // -----------------PRIVATE METHODS ---------------------------------------

        private SqlCommand BuildEmployeeLookupQueryCommand(EmployeeLookupRequestModel request)
        {
            var cmd = new SqlCommand();

            var queryBuilder = new StringBuilder(
                @"SELECT 
e.Id, 
e.Name Name, 
e.Position, 
m.Id ManagerId, 
m.Name ManagerName, 
a.City, 
a.StreetName, 
a.StreetNumber, 
(SELECT Count(*) FROM EMPLOYEE WHERE ManagerId = e.Id) EmployeesUnderManagement 
FROM Employee e 
LEFT JOIN Employee m on m.Id = e.ManagerId 
LEFT JOIN Address a on a.Id = e.AddressId ");

            var whereConditions = new List<string>();

            // Name param
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                whereConditions.Add($"e.Name LIKE @NameParam ");
                AddStringParam(cmd, "NameParam", $"%{request.Name}%");
            }

            // Position param
            if (!string.IsNullOrWhiteSpace(request.Position))
            {
                whereConditions.Add($"e.Position LIKE @PositionParam ");
                AddStringParam(cmd, "PositionParam", $"%{request.Position}%");
            }

            // ManagerName param
            if (!string.IsNullOrWhiteSpace(request.ManagerName))
            {
                whereConditions.Add($"m.ManagerName LIKE @ManagerNameParam ");
                AddStringParam(cmd, "ManagerNameParam", $"%{request.ManagerName}%");
            }

            // City param
            if (!string.IsNullOrWhiteSpace(request.City))
            {
                whereConditions.Add($"a.City LIKE @CityParam ");
                AddStringParam(cmd, "CityParam", $"%{request.City}%");
            }

            // StreetName param
            if (!string.IsNullOrWhiteSpace(request.StreetName))
            {
                whereConditions.Add($"a.StreetName LIKE @StreetNameParam ");
                AddStringParam(cmd, "StreetNameParam", $"%{request.StreetName}%");
            }


            // StreetNumber param
            if (!string.IsNullOrWhiteSpace(request.StreetNumber))
            {
                whereConditions.Add($"a.StreetNumber LIKE @StreetNumberParam ");
                AddStringParam(cmd, "StreetNumberParam", $"%{request.StreetNumber}%");
            }

            // EmployeesUnderManagement param
            if (request.EmployeesUnderManagement > -1)
            {
                whereConditions.Add($"(SELECT Count(*) FROM EMPLOYEE WHERE ManagerId = e.Id) = @EmployeesUnderManagementParam ");
                AddStringParam(cmd, "EmployeesUnderManagementParam", $"{request.EmployeesUnderManagement}");
            }



            if (whereConditions.Any())
            {
                var whereCondition = string.Join(" AND ", whereConditions);
                queryBuilder.Append("WHERE ");
                queryBuilder.Append(whereCondition);
            }

            request.OrderByColumn = request.OrderByColumn ?? "Name";

            if (!string.IsNullOrWhiteSpace(request.OrderByColumn))
                queryBuilder.Append($"ORDER BY {request.OrderByColumn} ");

            if (!request.OrderByAsc)
                queryBuilder.Append("DESC ");


            queryBuilder.Append($"OFFSET {request.PageSize * (request.PageNumber - 1)} ROWS ");

            queryBuilder.Append($"FETCH NEXT {request.PageSize} ROWS ONLY;");

            cmd.CommandText = queryBuilder.ToString();

            return cmd;
        }

        private void AddStringParam(SqlCommand cmd, string paramName, string paramValue)
        {
            var parameter = new SqlParameter
            {
                ParameterName = paramName,
                Value = paramValue,
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input
            };
            cmd.Parameters.Add(parameter);
        }

        private string BuildGetEmployeeByIDQuery()
        {
            return @"SELECT e.Id, e.Name Name, e.Position, m.Id ManagerId, m.Name ManagerName, a.City, a.StreetName, a.StreetNumber, (SELECT Count(*) FROM EMPLOYEE WHERE ManagerId = e.Id) EmployeesUnderManagement 
            FROM Employee e LEFT JOIN Employee m on m.Id = e.ManagerId LEFT JOIN Address a on a.Id = e.AddressId 
            WHERE e.Id = @IdParam";
        }
    }
}