using EmployeeManagementMC.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementMC.Repository.Abstract
{
    public interface IEmployeeRepository
    {
        Task<IList<EmployeeLookupResponseModel>> EmployeesLookupAsync(EmployeeLookupRequestModel request);
        Task<EmployeeLookupResponseModel> GetEmployeeByIdAsync(Guid id);
        Task<EmployeeInsertResponseModel> AddNewEmployeeRecordAsync(EmployeeInsertRequestModel request);
        Task<EmployeeInsertResponseModel> UpdateEmployeeRecordAsync(Guid id, EmployeeInsertRequestModel request);
        Task DeleteEmployeeRecordAsync(Guid id);
    }
}
