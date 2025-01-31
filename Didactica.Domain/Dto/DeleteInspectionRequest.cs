namespace Didactica.Domain.Dto;

/// <summary>
/// Represents the request to delete an inspection.
/// </summary>
/// <remarks>
/// This class is used to identify the specific inspection to be removed from the system
/// by providing its unique identifier.
/// </remarks>
public class DeleteInspectionRequest
{
   public required int InspectionId { get; init; }
}