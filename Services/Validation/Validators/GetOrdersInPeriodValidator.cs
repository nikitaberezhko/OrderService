using FluentValidation;
using Services.Models.Request;

namespace Services.Validation.Validators;

public class GetOrdersInPeriodValidator : AbstractValidator<GetOrdersInPeriodModel>
{
    public GetOrdersInPeriodValidator()
    {
        RuleFor(x => x.End).NotEmpty();

        RuleFor(x => x.Period)
            .NotEmpty()
            .GreaterThanOrEqualTo(28);
    }
}