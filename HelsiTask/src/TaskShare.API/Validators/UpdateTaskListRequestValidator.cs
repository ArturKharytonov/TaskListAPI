using FluentValidation;
using TaskShare.API.Requests;

namespace TaskShare.API.Validators;

public class UpdateTaskListRequestValidator : AbstractValidator<UpdateTaskListRequest>
{
    public UpdateTaskListRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Task list name is required")
            .MinimumLength(1).WithMessage("Name must be at least 1 characters long")
            .MaximumLength(255).WithMessage("Name cannot exceed 255 characters");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User ID must be a positive number");
    }
}