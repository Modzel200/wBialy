using FluentValidation;

namespace wBialy.Models.Validators
{
    public class CreateLFPostDtoValidator : AbstractValidator<CreateLFPostDto>
    {
        public CreateLFPostDtoValidator()
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
