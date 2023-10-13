using FluentValidation;

namespace wBialy.Models.Validators
{
    public class GastroTagDtoValidator : AbstractValidator<GastroTagDto>
    {
        public GastroTagDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
