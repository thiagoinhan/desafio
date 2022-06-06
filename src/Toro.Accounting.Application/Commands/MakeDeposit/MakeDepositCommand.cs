using Toro.Accounting.Domain.Commom;

namespace Toro.Accounting.Application.Commands
{
    public class MakeDepositCommand : ICommand<MakeDepositCommandResponse>
    {
        public MakeDepositCommand(string accountNumber, decimal amount)
        {
            AccountNumber = accountNumber;
            Amount = amount;
        }

        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
