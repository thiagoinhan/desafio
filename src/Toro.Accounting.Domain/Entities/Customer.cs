using Toro.Accounting.Domain.Commom;

namespace Toro.Accounting.Domain.Entities
{
    public class Customer : BaseEntity<Guid>
    {
        public static string TableName = "Customers";

        public Customer(Guid id, string login, string name, string cpf, string accountNumber, decimal accountBalance)
        {
            Id = id;
            Login = login;
            Name = name;
            CPF = cpf;
            AccountNumber = accountNumber;
            AccountBalance = accountBalance;
        }

        public string Login { get; private set; }
        public string Name { get; private set; }
        public string CPF { get; private set; }
        public string AccountNumber { get; private set; }
        public decimal AccountBalance { get; private set; }
        public void Deposit(decimal value)
        {
            AccountBalance += value;
        }
    }
}
