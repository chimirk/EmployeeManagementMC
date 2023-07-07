using EmployeeManagementMC.Core.Employees;
using EmployeeManagementMC.Repository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementMC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeLookupResponseModel>> GetEmployeeByIdAsync(Guid id)
        {
            var results = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeInsertResponseModel>> AddNewEmployeeRecordAsync(EmployeeInsertRequestModel request)
        {
            var result = await _employeeService.AddNewEmployeeRecordAsync(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeInsertResponseModel>> UpdateEmployeeRecordAsync(Guid id, EmployeeInsertRequestModel request)
        {
            // Remember that HTTP PUT is idempotent
            var results = await _employeeService.UpdateEmployeeRecordAsync(id, request);

            return Ok(results);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployeeRecordAsync(Guid id)
        {
            await _employeeService.DeleteEmployeeRecordAsync(id);
            return NoContent();
        }

        [HttpPost("lookup")]
        public async Task<ActionResult<List<EmployeeLookupResponseModel>>> EmployeesLookupAsync(EmployeeLookupRequestModel request)
        {
            var results = await _employeeService.EmployeesLookupAsync(request);
            return Ok(results);
        }


    }
}
