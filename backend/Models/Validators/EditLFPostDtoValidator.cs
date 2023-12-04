using FluentValidation;

namespace wBialy.Models.Validators
{
    public class EditLFPostDtoValidator : AbstractValidator<EditLFPostDto>
    {
        public EditLFPostDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(x => x.Place)
                .NotEmpty();
            RuleFor(x => x.Location)
                .NotEmpty();
            RuleFor(x => x.Tags)
                .NotEmpty();
            RuleFor(x => x.Found)
                .Must(y => y == false || y == true);
        }
    }
}
