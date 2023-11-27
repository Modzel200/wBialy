using FluentValidation;

namespace wBialy.Models.Validators
{
    public class EditEventPostDtoValidator : AbstractValidator<EditEventPostDto>
    {
        public EditEventPostDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000);
            RuleFor(x => x.Place)
                .NotEmpty();
            RuleFor(x => x.EventDate)
                .NotEmpty();
            RuleFor(x => x.Location)
                .NotEmpty();
            RuleFor(x => x.Tags)
                .NotEmpty();
        }
    }
}
