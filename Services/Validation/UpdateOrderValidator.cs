using FluentValidation;
using Services.Models.Request;

namespace Services.Validation;

public class UpdateOrderValidator : AbstractValidator<UpdateOrderModel>
{
    public UpdateOrderValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty);
        
        RuleFor(x => x.DateStart).NotEmpty();
        
        RuleFor(x => x.DateEnd).NotEmpty().GreaterThanOrEqualTo(x => x.DateStart);
        
        RuleFor(x => x.HubStartId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
        
        RuleFor(x => x.HubEndId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
        
        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(0);
        
        RuleFor(x => x.Containers).Must(x => x.Count > 0);
    }
}