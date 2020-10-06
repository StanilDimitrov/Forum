using FluentValidation;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Common;

namespace Forum.Application.PublicUsers.PublicUsers.Commands.Edit
{
    public class EditPublicUserCommandValidator : AbstractValidator<EditPublicUserCommand>
    {
        public EditPublicUserCommandValidator()
        {
            this.RuleFor(u => u.Email)
                .MinimumLength(MinEmailLength)
                .MaximumLength(MaxEmailLength)
                .EmailAddress()
                .NotEmpty();
        }
    }
}
