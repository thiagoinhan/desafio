using Toro.Accounting.Domain.Commom;

namespace Toro.Accounting.Application.Commands
{
    public class MakeDepositCommand : ICommand<MakeDepositCommandResponse>
    {
        public MakeDepositCommand(string accountNumber, string originCPF, decimal amount)
        {
            AccountNumber = accountNumber;
            Amount = amount;
            OriginCPF = originCPF;
        }

        public string AccountNumber { get; private set; }
        public decimal Amount { get; private set; }
        public string OriginCPF { get; private set; }
    }
}
