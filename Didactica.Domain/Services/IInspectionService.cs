using Didactica.Domain.Dto;
using FluentResults;

namespace Didactica.Domain.Services;

public interface IInspectionService
{
    Task<Result<GetInspectionResponse>> GetAsync(int id);

    Task<Result> AddAsync(CreateInspectionRequest request);
    
    Task<Result> DeleteAsync(DeleteInspectionRequest request);
    
    Task<Result<IEnumerable<GetInspectionResponse>>> GetInspectionsOfTeacherById(int teacherId);    
    Task<Result<IEnumerable<GetInspectionResponse>>> GetAllPLanedInspections();
}