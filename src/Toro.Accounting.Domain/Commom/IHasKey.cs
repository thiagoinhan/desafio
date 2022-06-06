namespace Toro.Accounting.Domain.Commom
{
    public interface IHasKey<T>
    {
        T Id { get; set; }
    }
}
