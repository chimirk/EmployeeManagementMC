using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementMC.Repository.Models
{
    public class EmployeeInsertRequestModel
    {
        public string Name { get; set; } = null!;
        public string Position { get; set; } = null!;
        public Guid? ManagerId { get; set; }
        public Guid? AddressId { get; set; }
    }
}
