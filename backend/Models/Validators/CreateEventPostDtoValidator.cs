using FluentValidation;

namespace wBialy.Models.Validators
{
    public class CreateEventPostDtoValidator : AbstractValidator<CreateEventPostDto>
    {
        public CreateEventPostDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(250);
            RuleFor(x => x.Place)
                .NotEmpty();
            RuleFor(x => x.Tags)
                .NotEmpty();
        }
    }
}
