using FluentValidation;

namespace Toro.Accounting.Application.Commands
{
    public class MakeDepositCommandValidator : AbstractValidator<MakeDepositCommand>
    {
        public MakeDepositCommandValidator()
        {
            
        }
    }
}
