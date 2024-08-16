using FluentValidation;
using Services.Models.Request;

namespace Services.Validation.Validators;

public class DeleteOrderValidator : AbstractValidator<DeleteOrderModel>
{
    public DeleteOrderValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}