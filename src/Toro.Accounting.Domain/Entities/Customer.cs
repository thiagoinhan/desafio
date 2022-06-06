using Toro.Accounting.Domain.Commom;

namespace Toro.Accounting.Domain.Entities
{
    public class Customer : BaseEntity<string>
    {
        public Customer(string id, string login, string name, string cpf, string accountNumber, decimal accountBalance)
        {
            Id = id;
            Login = login;
            Name = name;
            CPF = cpf;
            AccountNumber = accountNumber;
            _accountBalance = accountBalance;
        }

        public string Login { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string AccountNumber { get; set; }
        private decimal _accountBalance { get; set; }
        private decimal AccountBalance { get { return _accountBalance; } }
        public void Deposit(decimal value)
        {
            _accountBalance += value;
        }
    }
}
