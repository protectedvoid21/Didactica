using Didactica.Api.Dto;
using FluentResults;

namespace Didactica.Api.Service.Interfaces;

public interface IInspectionService
{
    Task<Result<GetInspectionResponse?>> GetAsync(int id);
}