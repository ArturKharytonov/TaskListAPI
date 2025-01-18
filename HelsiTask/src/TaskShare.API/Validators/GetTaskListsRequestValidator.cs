using FluentValidation;
using TaskShare.API.Requests;

namespace TaskShare.API.Validators;

public class GetTaskListsRequestValidator : AbstractValidator<GetTaskListsRequest>
{
    public GetTaskListsRequestValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User ID must be a positive number");

        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Page number must be greater than 0");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0")
            .LessThanOrEqualTo(100).WithMessage("Page size cannot exceed 100 items");
    }
}