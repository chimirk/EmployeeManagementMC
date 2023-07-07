using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementMC.Repository.Models
{
    public class EmployeeInsertResponseModel: EmployeeInsertRequestModel
    {
        public Guid Id { get; set; }
    }
}
