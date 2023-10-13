using FluentValidation;

namespace wBialy.Models
{
    public class LFTagDtoValidator : AbstractValidator<LFTagDto>
    {
        public LFTagDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
