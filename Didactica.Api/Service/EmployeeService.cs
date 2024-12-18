using Didactica.Persistence.Entities;

namespace Didactica.Api.Persistence.Service;

public class EmployeeService
{
    public readonly DidacticaDbContext DbContext;

    public EmployeeService(DidacticaDbContext dbContext)
    {
       DbContext = dbContext;
    }
    public async Task AddEmpleyee(Employee employee)
    {
        await DbContext.Employees.AddAsync(employee);
        await DbContext.SaveChangesAsync();
    }

    public IEnumerable<Employee> GetAllemployess()
    {
        return DbContext.Employees.AsEnumerable();

    }
}