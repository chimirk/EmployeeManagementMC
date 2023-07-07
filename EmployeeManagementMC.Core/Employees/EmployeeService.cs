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
        public async Task<EmployeeInsertResponseModel> AddNewEmployeeRecordAsync(EmployeeInsertRequestModel request)
        {
            return await _employeeRepository.AddNewEmployeeRecordAsync(request);
        }

        public async Task DeleteEmployeeRecordAsync(Guid id)
        {
            await _employeeRepository.DeleteEmployeeRecordAsync(id);
        }

        public async Task<IList<EmployeeLookupResponseModel>> EmployeesLookupAsync(EmployeeLookupRequestModel request)
        {
            return await _employeeRepository.EmployeesLookupAsync(request);
        }

        public async Task<EmployeeLookupResponseModel> GetEmployeeByIdAsync(Guid id)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(id);
        }

        public async Task<EmployeeInsertResponseModel> UpdateEmployeeRecordAsync(Guid id, EmployeeInsertRequestModel request)
        {
            return await _employeeRepository.UpdateEmployeeRecordAsync(id, request);
        }
    }
}
