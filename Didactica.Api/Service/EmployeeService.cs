using Didactica.Api.Persistence;
using Didactica.Api.Persistence.Entities;

namespace Didactica.Api.Service;

public class EmployeeService
{
    public readonly DidacticaDbContext DbContext;

    public EmployeeService(DidacticaDbContext dbContext)
    {
       DbContext = dbContext;
    }
    public async Task AddEmpleyee(Teacher teacher)
    {
        await DbContext.Teachers.AddAsync(teacher);
        await DbContext.SaveChangesAsync();
    }

    public IEnumerable<Teacher> GetAllemployess()
    {
        return DbContext.Teachers.AsEnumerable();

    }
}