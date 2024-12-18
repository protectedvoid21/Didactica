using Didactica.Api.Persistence.Service;
using Didactica.Persistence.Entities;

using Microsoft.AspNetCore.Mvc;

namespace Didactica.Api.Persistence.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            await _employeeService.AddEmpleyee(employee);
            return Ok(new { Message = "Employee added successfully!" });
        }

        [HttpGet("all")]
        public IActionResult GetAllEmployees()
        {
            var employees = _employeeService.GetAllemployess();
            return Ok(employees);
        }
    }
}
