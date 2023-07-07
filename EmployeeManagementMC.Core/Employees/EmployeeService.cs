using EmployeeManagementMC.Repository.Abstract;
using EmployeeManagementMC.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementMC.Core.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public Task<EmployeeInsertResponseModel> AddNewEmployeeRecordAsync(EmployeeInsertRequestModel request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEmployeeRecordAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<EmployeeLookupResponseModel>> EmployeesLookupAsync(EmployeeLookupRequestModel request)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeLookupResponseModel> GetEmployeeByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeInsertResponseModel> UpdateEmployeeRecordAsync(Guid id, EmployeeInsertRequestModel request)
        {
            throw new NotImplementedException();
        }
    }
}
