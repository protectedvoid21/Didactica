using Didactica.Domain.Dto;
using FluentValidation;

namespace Didactica.Domain.Validators;

public class AddInspectionFormValidator : AbstractValidator<AddInspectionFormRequest>
{
    public AddInspectionFormValidator()
    {
        
    }
}