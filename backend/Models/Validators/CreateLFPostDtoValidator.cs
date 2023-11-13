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
                .MaximumLength(600);
            RuleFor(x => x.Place)
                .NotEmpty();
            RuleFor(x => x.Location)
                .NotEmpty();
            RuleFor(x => x.Tags)
                .NotEmpty();
        }
    }
}
