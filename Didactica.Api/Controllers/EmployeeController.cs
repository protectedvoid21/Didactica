using Didactica.Api.Persistence.Entities;
using Didactica.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace Didactica.Api.Controllers
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
        public async Task<IActionResult> AddEmployee([FromBody] Teacher teacher)
        {
            await _employeeService.AddEmpleyee(teacher);
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
