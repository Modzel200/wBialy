using FluentValidation;
using Microsoft.EntityFrameworkCore;
using wBialy.Entities;

namespace wBialy.Models.Validators
{
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator(AppDbContext dbContext)
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8);
            RuleFor(x => x.Password)
                .Equal(y => y.PasswordConfirm);
            RuleFor(x => x.ResetToken)
                .Custom(async (value, context) =>
                {
                    var tokenExists = dbContext.Users.Any(x => x.PasswordResetToken == value);
                    if (!tokenExists)
                    {
                        context.AddFailure("Reset token", "Invalid reset token");
                    }
                })
                .MinimumLength(10);
        }
    }
}
