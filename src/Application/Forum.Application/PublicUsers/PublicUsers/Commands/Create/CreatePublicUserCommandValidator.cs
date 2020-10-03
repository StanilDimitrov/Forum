using FluentValidation;
using static Forum.Domain.PublicUsers.Models.ModelConstants.Common;

namespace Forum.Application.PublicUsers.PublicUsers.Commands.Create
{
    public class CreatePublicUserCommandValidator : AbstractValidator<CreatePublicUserCommnad>
    {
        public CreatePublicUserCommandValidator()
        {
            this.RuleFor(u => u.UserName)
                .MinimumLength(MinNameLength)
                .MaximumLength(MaxNameLength)
                .NotEmpty();

            this.RuleFor(u => u.Email)
                .MinimumLength(MinEmailLength)
                .MaximumLength(MaxEmailLength)
                .Matches(EmailRegularExpression)
                .NotEmpty();
        }
    }
}
