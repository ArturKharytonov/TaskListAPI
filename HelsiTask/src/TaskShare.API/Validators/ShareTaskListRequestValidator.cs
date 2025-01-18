using FluentValidation;
using TaskShare.API.Requests;

namespace TaskShare.API.Validators;

public class ShareTaskListRequestValidator : AbstractValidator<ShareTaskListRequest>
{
    public ShareTaskListRequestValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User ID must be a positive number");

        RuleFor(x => x.UserIdToShare)
            .GreaterThan(0).WithMessage("User ID to share with must be a positive number")
            .NotEqual(x => x.UserId).WithMessage("Cannot share task list with yourself");
    }
}