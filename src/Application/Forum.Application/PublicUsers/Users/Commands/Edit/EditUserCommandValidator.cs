using FluentValidation;
using Forum.Application.PublicUsers.Users.Commands.Edit;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Common;

namespace CarRentalSystem.Application.Dealerships.Dealers.Commands.Edit
{
    public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator()
        {
            this.RuleFor(u => u.Email)
                .MinimumLength(MinEmailLength)
                .MaximumLength(MaxEmailLength)
                .Matches(EmailRegularExpression)
                .NotEmpty();
        }
    }
}
