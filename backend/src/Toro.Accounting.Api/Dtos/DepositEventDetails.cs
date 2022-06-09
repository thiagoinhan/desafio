namespace Toro.Accounting.Api.Dtos
{
    public record DepositEventDetails(string @event, TargetAccountDetails target, OriginAccountDetails origin, decimal amount);
    public record TargetAccountDetails(string bank, string branch, string account);
    public record OriginAccountDetails(string bank, string branch, string cpf);
}
