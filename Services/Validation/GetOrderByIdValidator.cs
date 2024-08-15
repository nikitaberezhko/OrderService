using FluentValidation;
using Services.Models.Request;

namespace Services.Validation;

public class GetOrderByIdValidator : AbstractValidator<GetOrderByIdModel>
{
    public GetOrderByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}