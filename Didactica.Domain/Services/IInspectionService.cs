using Didactica.Domain.Dto;
using FluentResults;

namespace Didactica.Domain.Services;

public interface IInspectionService
{
    Task<Result<GetInspectionResponse>> GetAsync(int id);

    Task<Result> AddAsync(CreateInspectionRequest request);
}