using FluentValidation;

namespace wBialy.Models.Validators
{
    public class EventTagDtoValidator : AbstractValidator<EventTagDto>
    {
        public EventTagDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
