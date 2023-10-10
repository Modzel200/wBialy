using FluentValidation;

namespace wBialy.Models.Validators
{
    public class EditPostDtoValidator : AbstractValidator<EditPostDto>
    {
        public EditPostDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(250);
            RuleFor(x => x.Place)
                .NotEmpty();
            RuleFor(x => x.EventDate)
                .NotEmpty();
        }
    }
}
