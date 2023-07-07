using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementMC.Repository.Models
{
    public class EmployeeLookupResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Position { get; set; }
        public Guid? ManagerId { get; set; }
        public string? ManagerName { get; set; }
        public string? City { get; set; }
        public string? StreetName { get; set; }
        public string? StreetNumber { get; set; }
        public int EmployeesUnderManagement { get; set; }
    }
}
