using FluentValidation;
using Services.Models.Request;

namespace Services.Validation.Validators;

public class GetOrdersByClientIdValidator : AbstractValidator<GetOrdersByClientIdModel>
{
    public GetOrdersByClientIdValidator()
    {
        RuleFor(x => x.ClientId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}