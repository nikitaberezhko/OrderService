using FluentValidation;
using Services.Models.Request;

namespace Services.Validation;

public class GetAllOrdersValidator : AbstractValidator<GetAllOrdersModel>
{
    public GetAllOrdersValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        
        RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(50);
    }
}