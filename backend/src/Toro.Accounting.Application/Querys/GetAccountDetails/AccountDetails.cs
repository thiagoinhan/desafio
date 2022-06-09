namespace Toro.Accounting.Application.Dtos
{
    public record AccountDetails(string bank, string branch, Guid id, string userName, string cpf, string accountNumber, decimal accountBalance);
}
