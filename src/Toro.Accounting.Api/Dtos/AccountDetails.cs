namespace Toro.Accounting.Api.Dtos
{
    public record AccountDetails(string bank, string branch, string accountNumber, string name, decimal amount);
}
