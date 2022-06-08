namespace Toro.Accounting.Application.Dtos
{
    public record AccountDetails(string bank, string branch, string userName, string accountNumber, decimal accountBalance);
}
