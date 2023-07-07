using EmployeeManagementMC.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementMC.Core.Employees
{
    public interface IEmployeeService
    {
        Task<IList<EmployeeLookupResponseModel>> EmployeesLookupAsync(EmployeeLookupRequestModel request);
        Task<EmployeeLookupResponseModel> GetEmployeeByIdAsync(Guid id);
        Task<EmployeeInsertResponseModel> AddNewEmployeeRecordAsync(EmployeeInsertRequestModel request);
        Task<EmployeeInsertResponseModel> UpdateEmployeeRecordAsync(Guid id, EmployeeInsertRequestModel request);
        Task DeleteEmployeeRecordAsync(Guid id);
    }
}
