namespace Toro.Accounting.Domain.Commom
{
    public static class StringExtensions
    {
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}
