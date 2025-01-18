using FluentValidation;
using TaskShare.API.Requests;

namespace TaskShare.API.Validators;

public class UnshareTaskListRequestValidator : AbstractValidator<UnshareTaskListRequest>
{
    public UnshareTaskListRequestValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User ID must be a positive number");

        RuleFor(x => x.UserIdToUnshare)
            .GreaterThan(0).WithMessage("User ID to unshare with must be a positive number")
            .NotEqual(x => x.UserId).WithMessage("Cannot unshare task list from yourself");
    }
}