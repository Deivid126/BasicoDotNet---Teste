using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations
{
    public class RemoveAvisoRequestValidator : AbstractValidator<RemoveAvisoRequest>
    {
        public RemoveAvisoRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("O Id deve ser maior que zero.");
        }
    }
}
