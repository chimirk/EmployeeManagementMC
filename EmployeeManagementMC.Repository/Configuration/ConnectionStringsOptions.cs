namespace EmployeeManagementMC.Repository.Configuration;

public class ConnectionStringsOptions
{
    public string AdoConnectionString { get; set; } = null!;
    public string DapperConnectionString { get; set; } = null!;
}